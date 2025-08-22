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
  editModel: boolean = false;
  editStudent: any = { id: 0, firstName: '', lastName: '', email: '', phoneNumber: '' };

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

  openEditModal(student: any) {
    this.editModel = true;
    this.editStudent = { ...student };
  }

  closeEditModal(){
    this.editModel = false;
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
  updateStudent(){
      this.studentService.updateStudent(this.editStudent).subscribe({
        next:(res)=>{
          this.loadStudents();
          this.closeEditModal();
        },
        error:(err)=>{
          console.error('Error updating student:', err)
        }
      })
    }
    deleteStudent(id:number){
           this.studentService.deleteStudent(id).subscribe({
            next:(res)=>{
               this.loadStudents();
            },
            error:(err)=>{
              console.error('Error deleting student:', err)
            }
           })
    }
}
