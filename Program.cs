﻿

    

using System.Diagnostics;

// // Declare the variable as nullable with '?'
// // This tells the compiler: "I know this might be null. I accept responsibility."

// string? region = null;

// // Null-conditional operator '?.' — skip the call if null
// // If region is null, ToUpper() never executes. No crash.

// string? upperRegion = region?.ToUpper();
// Console.WriteLine($"Region(conditional):{upperRegion}");

// // Null-coalescing operator '??' — provide a fallback value
// // If region is null, use "Unassigned" instead.

// string displayRegion = region ?? "Unassigned";
// Console.WriteLine($"Region(coalesced):{displayRegion}");

// // Null-coalescing assignment '??=' — assign only if currently null
// // Useful for lazy initialization.

// region ??= "Addis Ababa";
// Console.WriteLine($"Region(assigned):{region}");

//****************Exercise 1 Lab session 1********************

// string studentName = "Abeba";
// string studentId = "STU-001";
// int enrollmentCount = 3;
// decimal grantAmount = 1999.99m; // 'm' suffix marks a decimal literal
// DateTime enrolledAt = DateTime.Now;
// string? campusRegion = null;

// Console.WriteLine($"Student: {studentName} ({studentId})");
// Console.WriteLine($"Courses: {enrollmentCount}");
// Console.WriteLine($"Grant: {grantAmount:F2}"); // Format as currency with 2 decimal places
// Console.WriteLine($"Enrolled:{enrolledAt:yyyy-mm-dd}");
// Console.WriteLine($"Campaus: {campusRegion ?? "Not assigned"}");

//****************Exercise 2 Lab session 1*********************

// decimal grantPerStudent = 1999.99m;
// decimal totalAllocation = grantPerStudent * 100_000m;

// Console.WriteLine($"Total allocated(double): {totalAllocation}");

//*****************exercise 3 Lab session 1****************************

// var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);
// Console.WriteLine(enrollment);

// var corrected = enrollment with { CourseCode = "CS-402" };
// Console.WriteLine(corrected);
// var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt);
// Console.WriteLine($"Same data? {enrollment == duplicate}"); // True


// var course = new Course{ Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
// Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");

// try
// {
//     course.Capacity =-5;
// }
// catch (ArgumentOutOfRangeException ex)
// {
//     Console.WriteLine($"Caught: {ex.Message}");
// }


// try
// {
//     course.Title = "";
// }
// catch (ArgumentException ex)
// {
//     Console.WriteLine($"Caught: {ex.Message}");


// }

// var s = new Student { Id = "S1", Name ="Abeba", Age = 20, GPA= 3.8m };
// Console.WriteLine($"Student: {s.Name}, GPA: {s.GPA}");
// ********************** exercise 3B  Lab session 1******************************
// void printGradeReport(IEnumerable<IGradable> assessments)
// {
//     Console.WriteLine("----Grade Report----");
//     foreach (var item in assessments)
//     {
//         Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");

//     }
// }

// IGradable[] cohortAssessments = [
//     new Quiz { Title = "C# Basics", CorrectAnswers = 18, TotalQuestions = 20 },
//     new LabAssignment {Title = "Registration API", FunctionalityScore = 90m, CodeQualityScore = 85m}
// ];

// printGradeReport(cohortAssessments);
//**********************************exercise 4 Lab session 2****************************
var service = new EnrollmentService();

var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
var validCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
var result = service.ProcessRegistration(validStudent, validCourse);
Console.WriteLine($"Enrolled: {result.StudentId} in {result.CourseCode}");

try
{
    service.ProcessRegistration(null, validCourse);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Caught: {ex.ParamName} cannot be null");
}
var fullCourse = new Course { Code = "CS-402", Title = "Full Course", Capacity = 1 };
fullCourse.EnrolledCount = 1;
try
{
    service.ProcessRegistration(validStudent, fullCourse);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Caught: {ex.Message}");
}


//********************exercise 5 Lab session 2****************************

 //List<Student> students = [
   //  new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m },
     //new Student { Id = "S2", Name = "Kidane", Age = 22, GPA = 3.5m },
     //new Student { Id = "S3", Name = "Dawit", Age = 19, GPA = 3.9m },
     //new Student { Id = "S1", Name = "Sara", Age = 20, GPA = 1.8m },
     //new Student { Id = "S2", Name = "Kebede", Age = 23, GPA = 3.5m },
     //new Student { Id = "S3", Name = "Mulu", Age = 19, GPA = 3.9m }
//];

//**************************exercise 6 Lab session 3****************************


var sw = Stopwatch.StartNew();
for (int i = 0; i < 5; i++)
{
    Thread.Sleep(300); // Thread is HELD for 300ms cannot serve anyone else

}
Console.WriteLine($"Blocking sequencial: {sw.ElapsedMilliseconds}ms");
// Async But still sequential: Thread released but calls are one at a time
sw.Restart();
for (int i = 0; i < 5; i++)
{
    await Task.Delay(300);//Thread released while waiting but still sequential
}
Console.WriteLine($"Async sequential: {sw.ElapsedMilliseconds}ms");
// THE RIGHT WAY:Asyncparallel all 5 start simultaneously
sw.Restart();
var tasks = Enumerable.Range(0, 5).Select(_ => Task.Delay(300));
await Task.WhenAll(tasks);
Console.WriteLine($"Async parallel: {sw.ElapsedMilliseconds}ms");

//********************exercise 6 Lab session 3 Build the TMS Student Fetcher****************************

async Task<Student> FetchStudentAsync(string id)
{
    Console.WriteLine($"Fetching {id}...");
    await Task.Delay(300);//simulate database latency
    return new Student
    {
        Id = id,
        Name = $"Student {id}",
        Age = 20,
        GPA = id switch
        {
            "S1" => 3.8m,
            "S2" => 2.4m,
            "S3" => 3.5m,
            "S4" => 1.9m,
            "S5" => 3.2m,
            _ => 2.5m
        }
    };
}

async Task<Course> FetchCourseAsync(string code)
{
Console.WriteLine($" Fetching course {code}...");
await Task.Delay(200); // Simulate database latency
return new Course
{
    Code = code,
Title = $"Course-{code}",
Capacity = code switch
{
"CRS-101" => 2,
"CRS-201" => 30,
"CRS-301" => 15,
_ =>25
}
};
}
sw.Restart();

// Start all fetches simultaneously students AND courses

string[] studentIds = ["S1", "S2", "S3", "S4", "S5"];
string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"];
var studentTasks = studentIds.Select(id => FetchStudentAsync(id));
var courseTasks = courseCodes.Select(code => FetchCourseAsync(code));

// Both arrays load concurrently

Student[] students = await Task.WhenAll(studentTasks);
Course[] courses = await Task.WhenAll(courseTasks);
Console.WriteLine($"\nLoaded {students.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms");
foreach (var s in students)
{
Console.WriteLine($" {s.Name} GPA: {s.GPA}");
}
//*************************************Exercise 6 Lab session 3 Step 3 Load in Parallel*************************

sw.Restart();

// start all Fetchs simultaneously student AND courses

// string[] studentIds = ["S1","S2","S3", "S4","S5"];
// string[] courseCodes = ["CRS-101", "CRS-201", "CRS-301"];

// var studentTasks = studentIds.Select(id => FetchStudentAsync(id));
// var courseTasks = courseCodes.Select(code => FetchCourseAsync(code));
// Both arrays Load Concurrently

Student[] students1 = await Task.WhenAll(studentTasks);
Course[] courses1 = await Task.WhenAll(courseTasks);
Console.WriteLine($"\nLoaded {students.Length} students and {courses.Length} courses in {sw.ElapsedMilliseconds}ms");
foreach (var s in students)
{
    Console.WriteLine($"{s.Name} GPA: {s.GPA}");

}


//****************************** Exercise 6 Part B: The TMS Enrollment Engine**********************************

var enrollCourse = new Course { Code = "CRS-101", Title = "C# Mastery", Capacity = 2 };
var enrollService = new EnrollmentService();

var enrollments = new List<EnrollmentRecord>();
var failures = new List<string>();

sw.Restart();

foreach (var student in students)
{
    try
    {
        var record = enrollService.ProcessRegistration(student, enrollCourse);
        enrollCourse.EnrolledCount++;
        enrollments.Add(record);
        Console.WriteLine($"Enrolled {student.Name}");
    }
    catch (InvalidOperationException ex)
    {
        failures.Add($"{student.Name}: {ex.Message}");
        Console.WriteLine($"Rejected: {student.Name}");
    }
        
}
//********Exercise 7 B**********
decimal classAverage = students.Length > 0
? students.Average(s => s.GPA)
: 0m;

Console.WriteLine("\n========== ENROLLMENT SUMMARY ==========");
Console.WriteLine($"Total students loaded:{students.Length}");
Console.WriteLine($"Successful enrollments: {enrollments.Count}");
Console.WriteLine($"Failed enrollments:{failures.Count}");
Console.WriteLine($"Class average GPA: {classAverage:F2}");
Console.WriteLine($"Total elapsed time: {sw.ElapsedMilliseconds}ms");
if (failures.Count > 0)
{
    Console.WriteLine("\n--- Failures ---");
    foreach (var fail in failures)
    {
        Console.WriteLine(fail);
    }
}
//************************************Exercise 6B: Safe Fire-and-Forget (Optional Not Assessed)**********************

async Task SendConfirmationAsync (Student student) {
try
{
await Task.Delay(100); // Simulate sending email
Console.WriteLine($" Email sent to {student.Name}");
}
catch (Exception ex) {
// Log the failure do NOT re-throw.
// This is intentional fire-and-forget.
Console.WriteLine($" Email failed for {student.Name}: {ex.Message}");
}
}
//*****************Exercise 7: The Unhelpful Crash (LO 1.8: Exceptions & Custom Faults)**************
try
{
    var overflowCourse = new Course { Code = "CRS-999", Title = "Overflow Course", Capacity = int.MaxValue };
    enrollService.ProcessRegistration(
        new Student { Id = "S99", Name = "Test", Age = 25, GPA = 3.0m }, overflowCourse);

}
catch (CapacityReachedException ex)
{
    Console.WriteLine($"\nDomain exception caught:");
    Console.WriteLine($" Course: {ex.CourseCode}");
    Console.WriteLine($" Message: {ex.Message}");
}