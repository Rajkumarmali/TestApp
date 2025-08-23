import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@node_modules/@angular/common';
import { CourseServicesServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-allcourse',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './allcourse.component.html',
  styleUrl: './allcourse.component.css'
})
export class AllcourseComponent implements OnInit {

     constructor(private courseService:CourseServicesServiceProxy,private changeDetector: ChangeDetectorRef){}
     courses:any[]=[];


     ngOnInit():void{
         this.loadCourse();
     }

     loadCourse(){
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
}
