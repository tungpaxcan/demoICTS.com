function textbox_autoheight(f, max, height) {
    var max = (typeof max == 'undefined') ? 1000 : max;
    if (f.scrollHeight > max) {
        if (f.style.overflowY != 'scroll') { f.style.overflowY = 'scroll' }
        return;
    }
    if (f.style.overflowY != 'hidden') { f.style.overflowY = 'hidden' }
    var scrollH = 0;
    if (height != 'undefined') {
        scrollH = f.scrollHeight + height;
    } else {
        scrollH = f.scrollHeight;
    }
    if (scrollH > f.style.height.replace(/[^0-9]/g, '')) {
        f.style.height = scrollH + 'px';
    }
}
String.prototype.replaceAll = function (replaceValue, newValue) {
    var functionReturn = this; while (true) {
        var currentValue = functionReturn;
        functionReturn = functionReturn.replace(replaceValue, newValue);
        if (functionReturn == currentValue)
            break;
    }
    return functionReturn;

};
function formatTime(textValue) {
    var functionReturn = new String(textValue);
    functionReturn = functionReturn.replaceAll(':', '');
    if (!isNaN(functionReturn)) {
        if (functionReturn.length >= 2) {

            var tempText = new String('');
            tempText = functionReturn.substr(0, 2) + ':';
            tempText += functionReturn.substr(2);
            functionReturn = tempText;

        }
        return functionReturn;
    }
    return "";
}
var dtCh = ".";
var minYear = 1900;
var maxYear = 9999;
function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}
function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}
function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}
function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}
function isDate(txt, dtStr) {
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strDay = dtStr.substring(0, pos1)
    var strMonth = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    //alert(day + "." + month + "." + year)
    //alert(pos1 + " " + pos2);
    if (pos1 == -1 || pos2 == -1) {
        $(txt).css('background-color', "#ffcc00");
        $(txt).attr('title', "Vui lòng nhập ngày theo định dạng dd.MM.yyyy !");
        return false
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        $(txt).css('background-color', "#ffcc00");
        $(txt).attr('title', "Vui lòng nhập tháng hợp lệ!");
        return false
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        //alert("asdfadsf");
        $(txt).css('background-color', "#ffcc00");
        $(txt).attr('title', "Vui lòng nhập ngày hợp lệ!");
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        $(txt).css('background-color', "#ffcc00");
        $(txt).attr('title', "Vui lòng nhập năm hợp lệ!");
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        $(txt).css('background-color', "#ffcc00");
        $(txt).attr('title', "Vui lòng nhập năm hợp lệ!");
        return false
    }
    $(txt).css('background-color', "Transparent");
    $(txt).attr('title', "");
    return true
}
function ValidateForm(txt) {
    if (isDate(txt, txt.value) == false) {
        //txt.focus()
        //$(txt).css('background-color', "#ffcc00");
        return false
    }
    return true
}
function ValidateFromGrid(dtStr) {
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strDay = dtStr.substring(0, pos1)
    var strMonth = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (pos1 == -1 || pos2 == -1) {
        return false
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        return false
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        return false
    }
    return true
}
function checkEmail(id) {
    var email = document.getElementById(id);
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email.value)) {
        return false;
    }
    return true;
}
function formatPhone(obj) {
    var numbers = obj.value.replace(/\D/g, ''),
        char = { 0: '(', 3: ') ', 6: '-' };
    obj.value = '';
    for (var i = 0; i < numbers.length; i++) {
        obj.value += (char[i] || '') + numbers[i];
    }
}
function writeCookie(name, value, days) {
    var date, expires;
    if (days) {
        date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    } else {
        expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}
function readCookie(name) {
    var i, c, ca, nameEQ = name + "=";
    ca = document.cookie.split(';');
    for (i = 0; i < ca.length; i++) {
        c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1, c.length);
        }
        if (c.indexOf(nameEQ) == 0) {
            return c.substring(nameEQ.length, c.length);
        }
    }
    return '';
}
function ReplaceSpecialCharacter(str) {
    var arrSpecialCharacter = ["!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+"];
    for (i = 0; i < arrSpecialCharacter.length; i++) {
        str = str.replaceAll(arrSpecialCharacter[i], "");
    }
    return str;
}
function Alert(message, icon) {
    var unique_id = $.gritter.add({
        title: 'Thông báo!',
        text: message,
        image: '/images/' + icon + '.png',
        sticky: true,
        time: '',
        class_name: 'my-sticky-class'
    });
    setTimeout(function () {
        $.gritter.remove(unique_id, {
            fade: true,
            speed: 'slow'
        });
    }, 12000);
   
} $(document).scroll(function () {
        $(".animated").addClass("delay-1s");
});
function validateEmail(email) {
    const res = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/g.test(email);
    if (res == false) {
        alert("Vui Lòng Nhập Đúng Định Dạng Email")
        return false;
    }
}