from zeep import Client
import http
import json
import requests


client = Client('http://localhost:9090/service.asmx?wsdl')

rest = ""


def SoapAddNewTeacher(client,tup):
    print(client.service.AddNewTeacher(tup))

def SoapGetAllTeachers(client):    
    print(client.service.GetAllTeachers())
    
def SoapGetTeacher(client,id):
    print(client.service.GetTeacher(id))

def SoabRemoveTeacher(client,id):
    print(client.service.RemoveTeacher(id))

def SoapUpdateTeacherInfo(client,id,teacher):
    print(client.service.UpdateTeacherInfo(id,teacher))


def RestGetStudentById(id):
   
    rest = requests.get("http://localhost:8080/students/"+str(id))
    print("Status: " , rest.status_code)
    
    if rest.status_code ==404:
        print("There is no such ID")
    else:
        restbyid = rest.json()
        print(restbyid['name'])


def RestGetAll():
    
    rest = requests.get("http://localhost:8080/students")
    restall = rest.json()
    print(restall)

def RestPost(name,age):
    
    rest = requests.post("http://localhost:8080/students", json ={"name":name, "age":age})
    print(rest.request.headers['Content-Type'])
    print("New row inserted: " , rest.request.body)
    
def RestPut(id,name,age):
    
    rest = requests.put("http://localhost:8080/students/"+str(id), json ={"name":name, "age":age})
    print(rest.request.headers['Content-Type'])
    print("Student list updated: " , rest.request.body)

def RestDelete(id):
    
    rest =requests.delete("http://localhost:8080/students/"+str(id))
    if rest.status_code ==404:
        print("There is no such ID")
    else:
        print("Student deleted: ")

print("*" * 150)
print("Press 1 for SoapWebService")
print("Press 2 for REST Webservice")
print("*" * 150)


continues = True

while continues == True:
    print("Main terminal")

    userinput = input()
    if userinput == "q":
        print("Client terminated!!")
        continues = False
    elif userinput =="1":
        print("+" * 150)
        print("Soap Webservice")
        print("Press 1 for get all teachers list")
        print("Press 2 for get teacher by id")
        print("Press 3 for add teacher to the list")
        print("Press 4 for update teachers list by id")
        print("Press 5 for delete a teacher from the list by id")       

        soap = True
        while soap == True:
            print("Soap Webservice main terminal")
            user = input()
            if user=="1":
                print("Get all teachers")
                SoapGetAllTeachers(client)
            elif user=="2":
                print("Get teacher by id")
                reply = input("Input id number ")
                try:
                    reply = int(reply)
                    SoapGetTeacher(client,reply) 
                    break
                except ValueError:
                    print('Input whole number')
                    soap = False       
              
            elif user=="3":
                print("Add new teacher to the list")
                replyid = 0 #input("Input id number ")
                replyname = input("Input name ")
                replyemail = input("Input email ")
                try:
                    replyid = int(replyid)
                    replyname.isalpha()
                    replyemail.isalpha()

                    newteacher = {"Id":replyid,"Name":replyname,"Email":replyemail}
                    SoapAddNewTeacher(client,newteacher)
                    print("New teacher added")                    
                    break
                except ValueError:
                    print('Invalid input')
                    soap = False               
                   
            elif user=="4":
                print("Update a given teacher")
                replyupdateid = input("Input id number ")
                replyupdatename = input("Input name ")
                replyupdateemail = input("Input email ")

                try:
                    replyupdateid = int(replyupdateid)
                    replyupdatename.isalpha()
                    replyupdateemail.isalpha()

                    updateteacher = {"Id":replyupdateid,"Name":replyupdatename, "Email":replyupdateemail}
                    SoapUpdateTeacherInfo(client,replyupdateid,updateteacher)
                    print("Teacher updated")                   
                    break
                except ValueError:
                    print('Invalid input')
                    soap = False                          
                    
            elif user=="5":
                print("Delete teacher by id")
                replydelete = input("Input id number ")

                try:
                    replydelete = int(replydelete)
                    SoabRemoveTeacher(client,replydelete)
                    print("Teacher deleted") 
                                      
                    break
                except ValueError:
                    print('Invalid input')
                    soap = False
            else:
                print("Input invalid - must be from 1 to 5")
                print("Soap terminal terminated!!")
                soap = False

        continues = True
    elif userinput =="2":
        print("-"*150)
        print("REST webservice")
        print("Press 1 for get all student list")
        print("Press 2 for get student by id")
        print("Press 3 for add student to the list")
        print("Press 4 for update student list by id")
        print("Press 5 for delete a student from the list by id")
      
        web = True
        while web==True:
            print("REST webservice main terminal")
            webuserinput = input()

            if webuserinput =="1":
                print("Get all students")
                RestGetAll()
            elif webuserinput=="2":
                print("Get student by ID")
                getid = input("Input id: ")

                try:
                    getid = int(getid)
                    RestGetStudentById(getid)                     
                    break
                except ValueError:
                    print('Input whole number')
                    soap = False   

                
            elif webuserinput == "3":
                print("Add a new student")
                newname = input("Input name: ")
                if not newname.isalpha():
                    print("Only letters are allowed!") 
                    break                            
                newage = input("Input age: ")

                try:
                    #newname.isalpha()
                    newage = int(newage)                  
                    RestPost(newname,newage)                                       
                    break
                except ValueError:
                    print('Invalid input')
                    soap = False 

                
            elif webuserinput == "4":
                print("Update student by ID")
                updateid = input("Input ID for update: ")
                updatename = input("Input name: ")
                updateage = input("Input age: ")

                try:
                    updateid = int(updateid)
                    updatename.isalpha()
                    updateage = int(updateage)
                    RestPut(updateid,updatename,updateage)
                    break
                except ValueError:
                    print('Invalid input')
                    soap = False  
              
            elif webuserinput == "5":
                print("Delete student")
                deleteid = input("Input id to delete: ")

                try:
                    deleteid = int(deleteid)
                    RestDelete(deleteid)                                        
                    break
                except ValueError:
                    print('Input whole number')
                    soap = False


        continues = True
    else:
        print("Invalid input - Client terminated!!")
        continues = False




    
    



