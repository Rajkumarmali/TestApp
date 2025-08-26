import { ChangeDetectorRef, Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { StuCourseServicesServiceProxy, StudentCreateDto, StudentServicesServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-student',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {
  students: any[] = [];
  studentProfile:any=null;
  studentCourses:any[]=[];
  searchTerm='';
  addModel: boolean = false;
  editModel: boolean = false;
  viewModel: boolean = false;
  editStudent: any = { id: 0, firstName: '', lastName: '', email: '', phoneNumber: '' };

  newStudent = {
   firstName: '',
   lastName: '',
   email: '',
   phoneNumber: ''
  }


  currentPage = 1;
  itemPage = 10;
  pages: number[] = [];

  constructor(
    private studentService: StudentServicesServiceProxy,
    private studentServices:StudentServicesServiceProxy,
    private stuCourseServices:StuCourseServicesServiceProxy,
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

  get filteredStudents() {
  if (!this.searchTerm.trim()) {
    return this.students;
  }
  const term = this.searchTerm.toLowerCase();
  return this.students.filter(s =>
    s.firstName.toLowerCase().includes(term) ||
    s.lastName.toLowerCase().includes(term) ||
    s.email.toLowerCase().includes(term) ||
    s.phoneNumber.toLowerCase().includes(term)
  );
}

  get paginatedStudents() {
     const start = (this.currentPage-1)*this.itemPage;
     return this.filteredStudents.slice(start,start+this.itemPage);
  }
   get totalPages(){
    return Math.ceil(this.students.length/this.itemPage);
   }


   changePage(page:number){
    if(page>=1 && page<=this.totalPages){
      this.currentPage = page;
    }
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

  closeViewModal() {
  this.viewModel = false;
  this.studentProfile = null;
  this.studentCourses = [];
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
    loadStuProfileAndCourse(email):void{
     this.studentServices.getStudentByEmain(email).subscribe({
        next:(res:any)=>{
          this.viewModel = true;
            this.studentProfile =res;
            this.changeDetector.detectChanges();
            console.log(this.studentProfile)

        } ,
        error:(err)=>{
          console.log(err);
        }
       })
      this.stuCourseServices.getEnrolledCourses(email).subscribe({
        next:(res)=>{
          this.studentCourses = res;
          this.changeDetector.detectChanges();
          console.log(this.studentCourses);

        },
        error:(err)=>{
          console.log(err);
        }
      })
    }
}
