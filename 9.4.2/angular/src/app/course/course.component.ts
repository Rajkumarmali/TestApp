import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@node_modules/@angular/common';
import { FormsModule } from '@node_modules/@angular/forms';
import { CourseServicesServiceProxy, CreateCouresDto, UpdateCourseDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-course',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './course.component.html',
  styleUrl: './course.component.css'
})
export class CourseComponent implements OnInit{
   courses:any[] =[]
   addModel:boolean = false;
   editModel:boolean=false;

   newCourse={
       name:'',
   }
   editCourse={
    id:0,
    name:''
   }

   constructor(private courseService:CourseServicesServiceProxy,private changeDetector: ChangeDetectorRef){}
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
}
