import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {Test} from "../../models/Test";
import {Topic} from "../../models/Topic";
import {MatTableDataSource} from "@angular/material/table";
import {TestService} from "../../services/test.service";
import {MatPaginator} from '@angular/material/paginator';
import {Router} from "@angular/router";
import {MatSnackBar} from "@angular/material/snack-bar";
import {FormBuilder} from "@angular/forms";

@Component({
  selector: 'test-create-form',
  templateUrl: './test.create.component.html',
  styleUrls: ['./test.create.component.scss'],
})

export class TestCreateComponent implements AfterViewInit {

  displayedColumns: string[] = ['name','actions'];

  @ViewChild(MatPaginator) paginator: MatPaginator;

  @ViewChild('testName') newTestNameInput:ElementRef;
  @ViewChild('topicName') newTopicNameInput:ElementRef;


  public test: Test

  public testForm = this.formBuilder.group({
    name: [''],
    description: ['']
  });

  public topicsDataSource: MatTableDataSource<Topic>

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private testService: TestService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.test = new Test();
    this.topicsDataSource = new MatTableDataSource<Topic>(this.test.topics);
  }

  ngAfterViewInit(): void {
    this.topicsDataSource.paginator = this.paginator;

    setTimeout(()=>{
      // this will make the execution after the above boolean has changed
      this.newTestNameInput.nativeElement.focus();
    },0);
  }

  validateTopicNameInput() : boolean{
    let value = this.newTopicNameInput.nativeElement.value;
    return value.length >= 3 && this.test.topics.filter(x=>x.name == value).length == 0;
  }

  addTopic(topicName: string){

    if (!this.validateTopicNameInput())
      return;

    let topic = new Topic();
    topic.name = topicName;
    this.test.topics.push(topic);

    this.newTopicNameInput.nativeElement.value = "";

    this.refreshTopicsTable();
  }

  refreshTopicsTable(){
    this.topicsDataSource.data = this.test.topics;
  }

  save(){

    this.testService
      .saveTest(this.testForm.value)
      .subscribe({
        next: value => {
          if (value.id > 0){
            this.router.navigate(['/lk']);
          }
        },
        error: err => {
          this.snackBar.open(err.message, "ok", { duration: 5 * 1000 });
        }
      });
  }

  removeTopic(topicName:string){
    if (window.confirm("Вы действительно хотите удалить топик?")) {
      let index = this.test.topics.findIndex(x=>x.name == topicName);
      if (index >= 0){
        this.test.topics.splice(index, 1);
        this.refreshTopicsTable();
      }
    }
  }

}
