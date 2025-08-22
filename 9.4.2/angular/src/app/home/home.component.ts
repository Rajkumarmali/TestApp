import { Component, Injector, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { CourseServicesServiceProxy, StudentServicesServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: './home.component.html',
  animations: [appModuleAnimation()],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HomeComponent extends AppComponentBase {
  totalStudent:number =0;
  totalCourse:number  = 0;
  constructor(injector: Injector,private studentService:StudentServicesServiceProxy,private courseService:CourseServicesServiceProxy,private  changeDetector: ChangeDetectorRef) {
    super(injector);
  }
  ngOnInit():void{
     this.studentService.getAllStudents().subscribe({
        next:(res:any)=>{
          this.totalStudent = res.length;
          this.changeDetector.detectChanges();
          console.log(this.totalStudent)
        },
        error:(err:any)=>{
          console.log(err);
        }
     })
     this.courseService.getAllCourses().subscribe({
      next:(res:any)=>{
        this.totalCourse = res.length;
        this.changeDetector.detectChanges();
        console.log(this.totalCourse)
      },
      error:(err:any)=>{
        console.log(err);
      }
     })
  }
}
