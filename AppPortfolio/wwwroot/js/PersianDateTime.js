(function () {
    var month_values = { "1": 1, "2": 2, "3": 3, "4": 4, "5": 5, "6": 6, "7": 7, "8": 8, "9": 9, "10": 10, "11": 11, "12": 12 };
    var $year = $("select#year");
    var $month = $("select#month");
    var $day = $("select#day");
    addYear();
    addMonth(month_values);

    $month.on('change', function () {
        var $year_selected = $('select#year').find(":selected").text();
        if (this.value <= 6)
            addDay(31);
        else if (7 <= this.value && this.value <= 11)
            addDay(30);
        else if (this.value == 12) {
            if ($year_selected % 4 === 3)
                addDay(30);
            else
                addDay(29);
        }

    });

    $year.on('change', function () {
        var $month_selected = $('select#month').find(":selected").text();

        if ($month_selected == 12 && this.value % 4 == 3)
            addDay(30);
        else if ($month_selected == 12 && this.value % 4 != 3)
            addDay(29);
    });

    function addDay(daysCount) {
        document.getElementById("day").options.length = 0;
        var date_day = new Date().getJalaliDate();
        for (var i = 1; i <= daysCount; ++i) {
            if (i === date_day)
                $day.append($("<option selected />").val(i).text(i));
            else
                $day.append($("<option />").val(i).text(i));
        }
    };

    function addMonth(month_values) {
        var date_month = new Date().getJalaliMonth();
        var $year_selected = $('select#year').find(":selected").text
        $.each(month_values, function (key, value) {
            if (value === date_month)
                $month.append($("<option selected />").val(key).text(value));
            else
                $month.append($("<option />").val(key).text(value));
        });
        if (date_month <= 6)
            addDay(31);
        else if (7 <= date_month && date_month <= 11)
            addDay(30);
        else if (date_month == 12) {
            if ($year_selected % 4 === 3)
                addDay(30);
            else
                addDay(29);
        }
    };

    function addYear() {
        var date_year = new Date().getJalaliFullYear();
        for (var i = date_year; i >= date_year - 10; --i) {
            if (i === date_year)
                $year.append($("<option selected />").val(i).text(i));
            else
                $year.append($("<option />").val(i).text(i));
        }
    };
});