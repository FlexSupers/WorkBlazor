using BlazorAppTestTask.Data;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using System.Text;
using System;
using System.Timers;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorAppTestTask.Pages
{
    public partial class TimerViewModel : ComponentBase
    {
        public DataTimer dataTimer = new DataTimer();
        public bool Flag { get; set; }

        System.Timers.Timer timer = new System.Timers.Timer(1);
        const string DEFAULT_TIME = "00:00:00";
        public string elapsedTime = DEFAULT_TIME;
        DateTime startTime = DateTime.Now;


        /*Реализация секундомера*/
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            DateTime currentTime = e.SignalTime;
            elapsedTime = $"{currentTime.Subtract(startTime)}";

            InvokeAsync(() =>
            {
                StateHasChanged();
            });
        }
        protected override void OnInitialized()
        {
            StartTimer();
        }

        public void StartTimer()
        {
            startTime = DateTime.Now;
            timer = new System.Timers.Timer(1);
            //timer.Interval = 2000;
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = false;
            //timer.Start();
            
        }

        public void Start()
        {
            //dataTimer.Seconds = dataTimer.Seconds - 1;
            //dataTimer.Minutes = dataTimer.Minutes - 1;
            //StateHasChanged();
            timer.Enabled = true;

        }


        public void Pause()
        {
            //timer.AutoReset = false;
            timer.Enabled = false;
        }

        public void Reset()
        {
            elapsedTime = "0";
        }

        /*Реализация таймера*/
        public void StartTime()
        {
            dataTimer.Seconds = dataTimer.Seconds - 1;
            dataTimer.Minutes = dataTimer.Minutes - 1;
            InvokeAsync(() =>
            {
                StateHasChanged();
            });

        }


        public void PauseTimer()
        {
            
        }

        public void ResetTimer()
        {
            dataTimer.Seconds = 0;
            dataTimer.Minutes = 0;
        }

    }
}
