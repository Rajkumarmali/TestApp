import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CourseServicesServiceProxy, StuCourseServicesServiceProxy, SessionServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-allcourse',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './allcourse.component.html',
  styleUrls: ['./allcourse.component.css']
})
export class AllcourseComponent implements OnInit {

  constructor(
    private courseService: CourseServicesServiceProxy,
    private stuCourseService: StuCourseServicesServiceProxy,
    private sessionService: SessionServiceProxy,
    private changeDetector: ChangeDetectorRef
  ) {}

  currentPage = 1;
  itemPage = 10;
  pages:number[]=[];

  courses: any[] = [];
  enrolledCourses: any[] = [];
  email: string = '';

  ngOnInit(): void {
    this.loadEmailAndCourses();
  }

  loadEmailAndCourses() {
    this.sessionService.getCurrentLoginInformations().subscribe({
      next: (res) => {
        this.email = res.user.emailAddress;
        this.loadCourses();
        this.loadEnrolledCourses();
      },
      error: (err) => console.log(err)
    });
  }

  loadCourses() {
    this.courseService.getAllCourses().subscribe({
      next: (res: any) => {
        this.courses = res;
        this.changeDetector.detectChanges();
      },
      error: (err) => console.log(err)
    });
  }

  get paginatedCourse() {
    const start = (this.currentPage-1)*this.itemPage;
    return this.courses.slice(start,start+this.itemPage);
  }

  get totalPages(){
    return Math.ceil(this.courses.length/this.itemPage);
  }
 changePage(page:number){
    if(page>=1 && page<=this.totalPages){
      this.currentPage = page;
    }
   }

  loadEnrolledCourses() {
    this.stuCourseService.getEnrolledCourses(this.email).subscribe({
      next: (res: any) => {
        this.enrolledCourses = res;
        this.changeDetector.detectChanges();
      },
      error: (err) => console.log(err)
    });
  }

  isEnrolled(course: any): boolean {
    return this.enrolledCourses?.some((c: any) => c.id === course.id);
  }

  enrollCourse(id:number) {
    this.stuCourseService.enrollInCourse(this.email,id).subscribe({
        next:(res:any)=>{
            this.loadEnrolledCourses();
            this.changeDetector.detectChanges();
        },
        error:(err)=>{
            console.log(err);
        }
    })
  }
}
