import { ChangeDetectorRef, Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StudentCreateDto, StudentServicesServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  students: any[] = [];
  addModel: boolean = false;
  newStudent = {
   firstName: '',
   lastName: '',
   email: '',
   phoneNumber: ''
  }

  constructor(
    private studentService: StudentServicesServiceProxy,
    private changeDetector: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.loadStudents();
  }

  loadStudents(): void {
    this.studentService.getAllStudents().subscribe({
      next: (res: any) => {
        this.students = res;
        console.log(this.students);
        this.changeDetector.detectChanges();
      },
      error: (err) => console.error(err)
    });
  }

  openAddModal() {
     this.addModel = true;
  }

  closeAddModel(){
    this.addModel = false;
    this.newStudent = { firstName: '', lastName: '', email: '', phoneNumber: '' };
  }
  
  addStudent(){
    const studentDto = new StudentCreateDto();
    studentDto.firstName = this.newStudent.firstName;
    studentDto.lastName = this.newStudent.lastName;
    studentDto.email = this.newStudent.email;
    studentDto.phoneNumber = this.newStudent.phoneNumber;

     this.studentService.createStudent(studentDto).subscribe({
       next:(res)=>{
        console.log('Student added:', res);
        this.loadStudents();
        this.closeAddModel();
       },
       error:(err)=>{
        console.error('Error adding student:', err);  
       }
     })
  }
}
