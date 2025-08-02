using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("How to Bake Bread", "Chef Anna", 420);
        video1.AddComment(new Comment("Emily", "This was so helpful, thanks!"));
        video1.AddComment(new Comment("James", "My bread came out perfect."));
        video1.AddComment(new Comment("Liam", "Nice tutorial, I'll try this."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Python Crash Course", "CodeWithLeo", 900);
        video2.AddComment(new Comment("Olivia", "Great intro to Python!"));
        video2.AddComment(new Comment("Ethan", "Loved how simple this was."));
        video2.AddComment(new Comment("Ava", "Could you make one for Java too?"));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Top 10 Football Goals", "SportsCentral", 360);
        video3.AddComment(new Comment("David", "Goal number 7 was insane!"));
        video3.AddComment(new Comment("Grace", "I remember watching these live."));
        video3.AddComment(new Comment("Noah", "Can't wait for next season."));
        videos.Add(video3);

        // Video 4 (optional)
        Video video4 = new Video("Beginner Guitar Lesson", "MusicMan", 540);
        video4.AddComment(new Comment("Sophia", "My fingers hurt but I'm learning!"));
        video4.AddComment(new Comment("Ben", "You made it easy to follow."));
        video4.AddComment(new Comment("Chloe", "I can play my first song now!"));
        videos.Add(video4);

        // Display all video info
        foreach (var video in videos)
        {
            video.DisplayVideo();
        }
    }
}
