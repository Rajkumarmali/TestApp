import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@node_modules/@angular/common';
import { FormsModule } from '@node_modules/@angular/forms';
import { CourseServicesServiceProxy, CreateCouresDto, StuCourseServicesServiceProxy, UpdateCourseDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-course',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './course.component.html',
  styleUrl: './course.component.css'
})
export class CourseComponent implements OnInit{
   courses:any[] =[]
   EnrolledStudents:any[]=[]
   addModel:boolean = false;
   editModel:boolean=false;
   viewModel:boolean=false;

   currentPage = 1;
   itemPage = 10;
   pages:number[]=[];

   newCourse={
       name:'',
   }
   editCourse={
    id:0,
    name:''
   }

   constructor(private stuCourseService:StuCourseServicesServiceProxy,private courseService:CourseServicesServiceProxy,private changeDetector: ChangeDetectorRef){}
   ngOnInit(){
     this.loadCourse();
   }

  openAddModel(){
     this.addModel = true;
  }
  openEditModel(course:any){
    this.editModel=true;
    this.editCourse={...course}
  }

  closeAddModel(){
    this.addModel = false;
    this.newCourse={name:''}
  }
  closeEditModel(){
    this.editModel=false;
    this.editCourse={id:0,name:''}
  }

   loadCourse(): void{
        this.courseService.getAllCourses().subscribe({
          next:(res:any)=>{
            this.courses = res;
            console.log(this.courses);
             this.changeDetector.detectChanges();
          },
          error:(err)=>{
            console.log(err);
          }
        })
   }

   get paginatedCourses(){
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
   saveCourse():void{
      const courseDto = new CreateCouresDto();
      courseDto.name = this.newCourse.name;
      this.courseService.createCourse(courseDto).subscribe({
        next:(res:any)=>{
          this.loadCourse();
          this.closeAddModel();
        },
        error:(err)=>{
          console.log(err);
        }
      })
   }

   updateCourse():void{
        const updateCourseDto = new UpdateCourseDto();
        updateCourseDto.id = this.editCourse.id;
        updateCourseDto.name = this.editCourse.name;
        this.courseService.updateCourse(updateCourseDto).subscribe({
          next:(res:any)=>{
              this.loadCourse();
              this.closeEditModel();
          },error:(err)=>{
            console.log(err);
          }
        })
   }
   deleteCourse(id:number):void{
       this.courseService.deleteCourse(id).subscribe({
        next:(res:any)=>{
           this.loadCourse();
        },
          error:(err)=>{
            console.log(err);
          }
       })
   }
    loadCourseDetandStu(id:number):void{
       this.stuCourseService.getAllEnrolledStudents(id).subscribe({
        next:(res:any)=>{
          this.EnrolledStudents = res;
          this.viewModel = true;
          this.changeDetector.detectChanges();
          console.log(this.EnrolledStudents)
        },
        error:(err)=>{
          console.log(err);
        }
       })
    }

    closeViewModel(){
      this.viewModel = false;
    }
}
