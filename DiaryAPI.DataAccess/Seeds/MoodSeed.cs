using DiaryAPI.Entities.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DiaryAPI.DataAccess.Seeds;

public class MoodSeed
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {
        var moodRepository = serviceProvider.GetRequiredService<IMoodRepository>();

        await SeedMoodItem("Mutlu", "\uD83D\uDE00", "#FFD700", 90, moodRepository);
        await SeedMoodItem("Üzgün", "\uD83D\uDE22", "#4682B4", 20, moodRepository);
        await SeedMoodItem("Kızgın", "\uD83D\uDE21", "#DC143C", 15, moodRepository);
        await SeedMoodItem("Aşık", "\uD83E\uDD70", "#FF69B4", 85, moodRepository);
        await SeedMoodItem("Şaşkın", "\uD83D\uDE32", "#FFA500", 50, moodRepository);
        await SeedMoodItem("Nötr", "\uD83D\uDE10", "#808080", 50, moodRepository);
        await SeedMoodItem("Yorgun", "\uD83D\uDE29", "#696969", 30, moodRepository);
        await SeedMoodItem("Rahat", "\uD83D\uDE0C", "#98FB98", 70, moodRepository);
        await SeedMoodItem("Çılgın", "\uD83E\uDD2A", "#FF4500", 60, moodRepository);
        await SeedMoodItem("Kırgın", "\uD83D\uDE1E", "#6495ED", 25, moodRepository);
        await SeedMoodItem("İçten", "\uD83D\uDE0A", "#87CEEB", 75, moodRepository);
        await SeedMoodItem("İyi", "\uD83D\uDE42", "#32CD32", 80, moodRepository);

    }

    private static async Task SeedMoodItem(
        string title,
        string unicode,
        string hexColor,
        int value,
        IMoodRepository moodRepository)
    {
        var moodEntity = await moodRepository.GetSingleAsync(x => x.Unicode == unicode);
        if (moodEntity != null)
        {
            moodEntity.Title = title;
            moodEntity.Value = value;
            moodEntity.HexColor = hexColor;
            await moodRepository.SaveAsync();
            return;
        }

        moodEntity = new(title, unicode, hexColor, value);
        await moodRepository.AddAsync(moodEntity);
        await moodRepository.SaveAsync();
    }
}