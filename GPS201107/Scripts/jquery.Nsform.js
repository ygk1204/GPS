//자바스크립트 1:N 입력 객체
/*
1:N 관계에서 N의 item을 입력폼을 생성과 제거하는 jquery 
*/
// 사용법
// .Nsform_area : Nsform들을 담을 HTML DOM class 명
// area : Nsform 영역을 담는 DOM class명
// form : Nsform 영역 class명
// item : Nsform 영역 안의 input tag 요소의 class명
// btn_add : 추가 버튼의 class명
// btn_delete : 삭제 버튼의 class명
// BeforeAdd : 추가 작업을 하기 전에 실행되는 함수 return 값이 false면 추가 작업 안함
// AfterAdd : 추가 작업을 완료 후 실행되는 함수
// BeforeDelete : 삭제 작업을 하기 전에 실행되는 함수 return 값이 false delete 작업 안함
// AfterDelete : 삭제 작업을 완료 후 실행되는 함수
// ex)
//  $(".Nsitem_area").Nsform({
//      form: "NsItem_form"
//    , item: "NsItem_item"
//    , btn_add: "NsItem_btn_add"
//    , btn_delete: "NsItem_btn_delete"
//    , BeforeAdd: function () {
//    }
//    , BeforeRemove: function () {
//    }
//    , AfterAdd: function (newItems) {
//    }
//    , AfterRemove: function () {
//    }
//});

function ArrangeNamesBeforeSubmit(form_class, item_class) {

    var className = "." + form_class;
    $(className).each(function (i) {              // 삭제와 추가로 인한 다시 아이템 배열 인덱스 번호 정리 
        $(this).find("." + item_class).each(function () {         //아이템 안에 자식요소들을 각각 확인 --> name속성 안에 '['값이 있는 것들 찾기
            var names = $(this).attr("name");                  //바뀌기 전의 네임속성의 값

            // 대괄호 숫자 뽑아내기
            var array_names_left = names.split('[');
            var array_names_right = array_names_left[1].split(']');
            var index = array_names_right[0];                  //바뀌기 전의 아이템 네임 속성의 배열 인덱스번호 가져오기 -> 문자열 중 숫자 뽑기
            var newNames = names.split(index).join(i);         // 배열 인덱스 번호를 순서 맞게 바꾼 값

            $(this).attr('name', newNames);                    // 정리된 익덱스 번호 값을 갖는 네임속성에 할당
            $(this).attr('id', newNames);
        });
    });
}

(function ( $ ) {

    $.fn.Nsform = function (target) {
    
        // private member variable
        var _terget = $(this);    
        var _target_area_class = "." + target.area;
        var _target_form_class = "." + target.form;
        var _target_item_class = "." + target.item;
        var _target_btn_add_class = "." + target.btn_add;
        var _target_btn_delete_class = "." + target.btn_delete;
    
        // private method
        var _BeforeAdd = function () {
            return true;
        }
        var _BeforeDelete = function () {
            return true;
        }
        var _AfterAdd = function (newItems) {
        }
        var _AfterDelete = function () {
        }
    
        // asigned overrding method for object
        if (target.BeforeAdd != null) {
            _BeforeAdd = target.BeforeAdd;
        }
        if (target.BeforeDelete != null) {
            _BeforeDelete = target.BeforeDelete;
        }
        if (target.AfterAdd != null) {
            _AfterAdd = target.AfterAdd;
        }
        if (target.AfterDelete != null) {
            _AfterDelete = target.AfterDelete;
        }
    
        var _ArrangeNamesBeforeSubmit = function (nameofclass, itemnameclass) {
    
            var className = nameofclass;
            $(className).each(function (i) {              // 삭제와 추가로 인한 다시 아이템 배열 인덱스 번호 정리 
                $(this).find(itemnameclass).each(function () {         //아이템 안에 자식요소들을 각각 확인 --> name속성 안에 '['값이 있는 것들 찾기
                    var names = $(this).attr("name");                  //바뀌기 전의 네임속성의 값
    
                    // 대괄호 숫자 뽑아내기
                    var array_names_left = names.split('[');
                    var array_names_right = array_names_left[1].split(']');
                    var index = array_names_right[0];                  //바뀌기 전의 아이템 네임 속성의 배열 인덱스번호 가져오기 -> 문자열 중 숫자 뽑기
                    var newNames = names.split(index).join(i);         // 배열 인덱스 번호를 순서 맞게 바꾼 값
    
                    $(this).attr('name', newNames);                    // 정리된 익덱스 번호 값을 갖는 네임속성에 할당
                    $(this).attr('id', newNames);
                });
            });
        }
    
        //item을 추가할 때
        $(_target_btn_add_class).click(function () {
    
            var vResult = _BeforeAdd();
    
            if (vResult == false) {
                return false;
            }
    
            var index = $(_target_btn_add_class).index(this); //add btn index
            var AreaAdd = $(_target_area_class).eq(index); // index add area
            var preitem = AreaAdd.find(_target_form_class).last();
            var newItem = AreaAdd.find(_target_form_class).last().clone(true); // 복사 할  아이템
    
            newItem.insertAfter(preitem); // 복사한 item을 그 전의 마지막 item 다음으로 위치시킨다.
    
            newItem.find('[name*="["]').each(function () {  // item의 자식들 중에서 name 속성에 [가 있는 것들을 찾아 반복문을 사용한다.            
                if ($(this).is(":checkbox")) {
                    $(this).prop('checked', false);
                }
                else {
                    $(this).val('');                                       // 복사 되어 그 전에 있던 값들 없앤다.
                }
            });
    
            //세부 사항 
            _ArrangeNamesBeforeSubmit(_target_form_class, _target_item_class);
    
            _AfterAdd(newItem);
        });
    
        //item을 삭제할 때
        $(_target_btn_delete_class).click(function () {   //Remove 클릭할때
    
            var vResult = _BeforeDelete();
    
            if (vResult == false) {
                return false;
            }
    
            var index = $(_target_btn_delete_class).index(this); // delete btn index
            var removeItem = $(_target_form_class).eq(index);                  //삭제 할 아이템        
    
            if ($(_target_area_class).find(_target_form_class).length == 1) {
                alert("더 이상 삭제 할 수 없습니다.")
            }
            else {
                removeItem.remove();                                           //아이템 제거
                _ArrangeNamesBeforeSubmit(_target_form_class, _target_item_class);
                _AfterDelete();
            }
    

        });
    };
}( jQuery ));