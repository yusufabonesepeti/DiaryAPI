using DiaryAPI.Entities.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryAPI.DataAccess.Seeds;

public class MoodSeed
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var moodRepository = serviceProvider.GetRequiredService<IMoodRepository>();

        await SeedMoodItem("Mutlu", "\uD83D\uDE00", 90, moodRepository);
        await SeedMoodItem("Üzgün", "\uD83D\uDE22", 20, moodRepository);
        await SeedMoodItem("Kızgın", "\uD83D\uDE21", 15, moodRepository);
        await SeedMoodItem("Aşık", "\uD83E\uDD70", 85, moodRepository);
        await SeedMoodItem("Şaşkın", "\uD83D\uDE32", 50, moodRepository);
        await SeedMoodItem("Nötr", "\uD83D\uDE10", 50, moodRepository);
        await SeedMoodItem("Yorgun", "\uD83D\uDE29", 30, moodRepository);
        await SeedMoodItem("Rahat", "\uD83D\uDE0C", 70, moodRepository);
        await SeedMoodItem("Çılgın", "\uD83E\uDD2A", 60, moodRepository);
        await SeedMoodItem("Kırgın", "\uD83D\uDE1E", 25, moodRepository);
        await SeedMoodItem("İçten", "\uD83D\uDE0A", 75, moodRepository);
        await SeedMoodItem("İyi", "\uD83D\uDE42", 80, moodRepository);  

    }

    private static async Task SeedMoodItem(
        string title,
        string unicode,
        int value,
        IMoodRepository moodRepository)
    {
        var moodEntity = await moodRepository.GetSingleAsync(x => x.Unicode == unicode);
        if (moodEntity != null) return;

        moodEntity = new(title, unicode, value);
        await moodRepository.AddAsync(moodEntity);
        await moodRepository.SaveAsync();
    }
}