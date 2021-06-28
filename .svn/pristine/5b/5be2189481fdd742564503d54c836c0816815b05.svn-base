$(document).ready(function () {
    //텍스트 입력 문자 제한
    $('input[type=text], textarea').keydown(function () {
        var text_content = $(this).val();
        var length_text = $(this).val().length;
        var numofbyte = cal_length(text_content);
        if (numofbyte > 4000) {
            alert("입력한 값이 4000byte를 넘었습니다. ");
            $(this).val(text_content.substr(0, length_text - 2));
            return false;
        }
    });
});
     function cal_length(val) {
         // 입력받은 문자열을 escape() 를 이용하여 변환한다.
         // 변환한 문자열 중 유니코드(한글 등)는 공통적으로 %uxxxx로 변환된다.
         var temp_estr = escape(val);
         var s_index = 0;
         var e_index = 0;
         var temp_str = "";
         var cnt = 0;

         // 문자열 중에서 유니코드를 찾아 제거하면서 갯수를 센다.
         while ((e_index = temp_estr.indexOf("%u", s_index)) >= 0) // 제거할 문자열이 존재한다면
         {
             temp_str += temp_estr.substring(s_index, e_index);
             s_index = e_index + 6;
             cnt++;
         }

         temp_str += temp_estr.substring(s_index);
         temp_str = unescape(temp_str); // 원래 문자열로 바꾼다.
         // 유니코드는 2바이트 씩 계산하고 나머지는 1바이트씩 계산한다.
         return ((cnt * 2) + temp_str.length) + "";
     }