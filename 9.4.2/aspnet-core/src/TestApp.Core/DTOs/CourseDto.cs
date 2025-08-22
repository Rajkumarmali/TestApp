namespace TestApp.DTOs;

public class CreateCouresDto
{
    public string Name { get; set; }
}

public class GetCourseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
}

public class UpdateCourseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
}