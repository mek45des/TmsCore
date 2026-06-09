class EnrollmentService
{
    public EnrollmentRecord ProcessRegistration(Student student, Course course)
    {
        if (student is null) throw new ArgumentNullException(nameof(student));
        if (course is null) throw new ArgumentNullException(nameof(course));
        if (course.EnrolledCount >= course.Capacity)
            throw new CapacityReachedException(course.Code);
        string standing = student.GPA switch
        {
            >= 3.5m => "Excellent",
            >= 2.5m => "Good Standing",
            _ => "Below Average"
        };

        Console.WriteLine($"{student.Name} is in {standing} with GPA: {student.GPA}");

        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
    }
}