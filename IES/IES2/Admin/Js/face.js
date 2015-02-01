var faceIsShow = false;//表情显示隐藏状态
var faceSrcPrefix = '../../Content/Images/face/';//表情路径
var $faceTextArea;//当前编辑文本对象
var faceData = {
    '[织]': 'zz2_thumb.gif', '[神马]': 'horse2_thumb.gif', '[浮云]': 'fuyun_thumb.gif', '[给力]': 'geili_thumb.gif',
    '[玫瑰]': 'mg_thumb.gif', '[凋谢]': 'dx_thumb.gif', '[熊猫]': 'panda_thumb.gif', '[兔子]': 'rabbit_thumb.gif',
    '[奥特曼]': 'otm_thumb.gif', '[囧]': 'j_thumb.gif', '[互粉]': 'hufen_thumb.gif', '[礼物]': 'liwu_thumb.gif',
    '[呵呵]': 'smilea_thumb.gif', '[嘻嘻]': 'tootha_thumb.gif', '[哈哈]': 'laugh.gif', '[可爱]': 'tza_thumb.gif',
    '[可怜]': 'kl_thumb.gif', '[挖鼻屎]': 'kbsa_thumb.gif', '[吃惊]': 'cj_thumb.gif', '[害羞]': 'shamea_thumb.gif',
    '[挤眼]': 'zy_thumb.gif', '[闭嘴]': 'bz_thumb.gif', '[鄙视]': 'bs2_thumb.gif', '[爱你]': 'lovea_thumb.gif',
    '[泪]': 'sada_thumb.gif', '[偷笑]': 'heia_thumb.gif', '[亲亲]': 'qq_thumb.gif', '[生病]': 'sb_thumb.gif',
    '[太开心]': 'mb_thumb.gif', '[懒得理你]': 'ldln_thumb.gif', '[右哼哼]': 'yhh_thumb.gif', '[左哼哼]': 'zhh_thumb.gif',
    '[嘘]': 'x_thumb.gif', '[衰]': 'cry.gif', '[委屈]': 'wq_thumb.gif', '[吐]': 't_thumb.gif', '[打哈气]': 'k_thumb.gif',
    '[抱抱]': 'bba_thumb.gif', '[怒]': 'angrya_thumb.gif', '[疑问]': 'yw_thumb.gif', '[馋嘴]': 'cza_thumb.gif',
    '[拜拜]': '88_thumb.gif', '[思考]': 'sk_thumb.gif', '[汗]': 'sweata_thumb.gif', '[困]': 'sleepya_thumb.gif',
    '[睡觉]': 'sleepa_thumb.gif', '[钱]': 'money_thumb.gif', '[失望]': 'sw_thumb.gif', '[酷]': 'cool_thumb.gif',
    '[花心]': 'hsa_thumb.gif', '[鼓掌]': 'gza_thumb.gif', '[哼]': 'hatea_thumb.gif', '[心]': 'hearta_thumb.gif',
    '[伤心]': 'unheart.gif', '[猪头]': 'pig.gif', '[ok]': 'ok.gif', '[耶]': 'ye_thumb.gif', '[good]': 'good_thumb.gif',
    '[不要]': 'no.gif', '[赞]': 'z2_thumb.gif', '[来]': 'come_thumb.gif', '[蜡烛]': 'lazu_thumb.gif', '[钟]': 'clock_thumb.gif',
    '[蛋糕]': 'cake.gif', '[话筒]': 'm_thumb.gif', '[便便]': 'bb_thumb.gif', '[坏笑]': 'hx_thumb.gif',
    '[尴尬]': 'gg_thumb.gif', '[发怒]': 'fn_thumb.gif', '[手套]': 'shoutao_thumb.gif'
}

//匹配表情和html标签
var expandReplaceRegStr = "(\<.+?\>)|(\<\/.+?\>)|(\<.+?\/\>)";
var onlyFaceRegStr = "(\\[织\\])";
for (var f in faceData) {
    expandReplaceRegStr += "|(\\[" + f.substring(1, f.length - 1) + "\\])";
    onlyFaceRegStr += "|(\\[" + f.substring(1, f.length - 1) + "\\])";
}
var expandReplaceReg = new RegExp(expandReplaceRegStr, 'ig');
var onlyFaceReg = new RegExp(onlyFaceRegStr, 'ig');

closeFace();//绑定隐藏表情选择框
selectFace();//绑定选择表情

/**
 * 隐藏表情选择框
 */
function closeFace() {
    $('div.face_comment_area div.close_img').die("click").live("click", hideFace);
}

function bindDocumentClickBlur() {
    $('div.face_comment_area').unbind("click").bind("click", function (e) {
        stopBubble(e);
    });
}

/**
 * 载入表情选择框
 */
function loadFace($this) {
    var info = '<div class="face_comment_area" style="display:none;z-index:1000;">'
		+ '  <div class="face_fear_arrow"> <em class="face_w_arrline">◆</em><span>◆</span> </div>'
		+ '  <div class="face_fear_comments">'
		+ '    <div class="face_fear_comments_in clears">'
		+ '      <div class="face_box">'
		+ '        <div class="close">'
		+ '          <div class="close_img"><a href="javascript:hideFace()"></a></div>'
		+ '        </div>'
		+ '        <div class="face_set">'
		+ '          <ul>';
    for (var n in faceData) {
        info += '<li style="margin:0px;" title="' + n + '"></li>';
    }
    info += '          </ul>'
		+ '          <div style="clear:both;"></div>'
		+ '        </div>'
		+ '        <div class="face_list">'
		+ '        </div>'
		+ '      </div>'
		+ '    </div>'
		+ '  </div>'
		+ '</div>';
    $this.append(info);
}

/**
 * 将文本转换成表情图像
 * @param {Object} content
 * @return {TypeName} 
 */
function replaceStrToImg(content) {
    var info = '';
    var regex = onlyFaceReg;// /\[([\u4E00-\u9FA5\uF900-\uFA2D]|\w)+\]/g ;
    var faceNames = content.match(regex);
    if (faceNames != null) {
        var len = faceNames.length;
        for (var i = 0; i < len; i++) {
            var faceImg = faceData[faceNames[i]];
            var _faceName = faceNames[i].toString().match(/([\u4E00-\u9FA5\uF900-\uFA2D]|\w)+/)[0];
            content = content.replace(faceNames[i], '<img src="' + faceSrcPrefix + faceImg + '" title="' + _faceName + '" />');
        }
    }
    return content;
}

/**
 * 展开提问表情选择框
 * @memberOf {TypeName} 
 * @return {TypeName} 
 */
function showFace($this, $text) {
    var dian = able.reMouse();
    $this.unbind("click");
    $this.click(function (e) {
        if (faceIsShow == true) {
            hideFace();
            return false;
        }
        var location = $this.offset();// 当前元素位置
        var location_parent = $this.parent().offset();//外层父元素位置
        var p_location_top = $this.parent().offset().top;//外层父元素位置
        var _left = location_parent.left + 'px';
        var _top = location_parent.top + 25 + 'px';
        var _topH = location.top - 295 + 'px';
        var p_top = parseInt(location.top - p_location_top);
        if (p_top >= 325) {
            $('div.face_comment_area').css({ 'left': _left, 'top': _topH }).show();
            $('.face_fear_arrow').hide();
        } else {
            $('div.face_comment_area').css({ 'left': _left, 'top': _top }).show();
            //$('div.face_comment_area').css({ 'left': dian.x, 'top': dian.y }).show();
        }
        $faceTextArea = $text;
        faceIsShow = true;

        stopBubble($this);
    });
}

/**
 * 选择表情
 */
/*function selectFace(){
	$('div.face_box img').die("click").live("click",function(){
		var faceName = $(this).attr('title');
		var _offset = getCursor($faceTextArea[0]);
		if($faceTextArea.attr("tip")==$faceTextArea.val()){
			$faceTextArea.val("");
		}
		var msg = $faceTextArea.val();
		if(_offset == -1){
			$faceTextArea.val(msg+faceName).focus();
		}else{
			$faceTextArea.val(msg.substring(0,_offset)+faceName+msg.substr(_offset)).focus();
		}
		hideFace();
	});
}*/

/**
 * 选择表情
 */
function selectFace() {
    $('div.face_box li').unbind("click").bind("click", function () {

        var faceName = $(this).attr('title');
        var _offset = getCursor($faceTextArea[0]);
        if ($faceTextArea.attr("tip") == $faceTextArea.val()) {
            $faceTextArea.val("");
        }
        var msg = $faceTextArea.val();
        if (_offset == -1) {
            $faceTextArea.val(msg + faceName).focus();
        } else {
            $faceTextArea.val(msg.substring(0, _offset) + faceName + msg.substr(_offset)).focus();
        }
        hideFace();
    });
}

/**
 * 隐藏提问表情选择框
 */
function hideFace() {
    $('div.face_comment_area').hide();
    faceIsShow = false;
}

/**
 * 取得传入的textarea对象(js对象，不是jquery对象)中光标位置
 * @param {Object} textAreaObj
 * @return {TypeName} 
 */
function getCursor(textAreaObj) {
    // FF下
    var _offset = textAreaObj.selectionEnd;
    if (_offset) {
        return _offset;
    } else {
        return -1;
    }
}

function stopBubble(e) {
    //如果提供了事件对象，则这是一个非IE浏览器 
    if (e && e.stopPropagation) {
        //因此它支持W3C的stopPropagation()方法 
        e.stopPropagation();
    } else {
        //否则，我们需要使用IE的方式来取消事件冒泡 
        if (window.event)
            window.event.cancelBubble = true;
    }
}

able = {
    reEvent: function () {
        //获取Event
        return window.event ? window.event : (function (o) {
            do {
                o = o.caller;
            } while (o && !/^\[object[ A-Za-z]*Event\]$/.test(o.arguments[0]));
            return o.arguments[0];
        })(this.reEvent);
    },
    reMouse: function () {
        //获取鼠标位置
        var e = able.reEvent(), documentElement = document.documentElement;
        return {
            x: documentElement.scrollLeft + e.clientX,
            y: documentElement.scrollTop + e.clientY
        };
    }
}

