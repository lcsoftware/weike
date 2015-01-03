var OmGrid = function(){
    var me =this;
    var _id= "";
    var container;
    var items;
    var title;
    var ckb_manager;
    var item_manager;
    var select_manager;
    
    //-------------------------------------OmGrid基本方法---------------------------------------
    //封装ID属性
    var ID=function(id){
        if(id)_id="#"+id;
        return _id.replace('#','');
    };
    
    //解绑所有grid中的事件
    var unbindAll=function(){
        container.find("*").unbind();
    };
    
    //为容器增加样式
    var setGridContainer=function()
    {
        container.css("position","relative"); //添加相对定位用于定位下拉菜单
    };
    
    //加载基本样式和事件
    var InitItems=function(){
        items.each(function(index,domEle){
            //设置行ID
            if(!(jQuery(this).attr("itemID")))
            {
                jQuery(this).attr("itemID",jQuery(this).attr("id"));
                jQuery(this).removeAttr("id");
            }
            var item_me=this;
            var tempID=jQuery(this).attr("itemID");  //当前行ID

            if(jQuery(this).hasClass("item_row")) //列表样式时使用交替行样式
            jQuery(this).addClass(index%2==0?'item_even':'item_odd'); 
            
             //行hover样式
            jQuery(this).hover(function(){
                jQuery(this).addClass('item_over');
            },function(){
                jQuery(this).removeClass('item_over');
            });
            
            //为行内的item_jslist添加iframe用于遮挡select元素
            jQuery(this).find(".item_jslist").each(function(){
                var list_height=$(this).height();
                $(this).append("<iframe scrolling='no' height='"+list_height+"' class='iframe_cover' frameborder='0'></iframe>");
            });
            
            //可编辑文本框的样式写入
            jQuery(this).find(".item_input").focus(function(){
                jQuery(this).removeClass("item_inputlabel");
            });
            jQuery(this).find(".item_input").blur(function(){
                jQuery(this).addClass("item_inputlabel");
            });
            
            //行hover显示下拉按钮
            jQuery(this).hover(function(){
                jQuery(this).find(".item_btn").show();
            },function(){
                jQuery(this).find(".item_btn").hide();
                jQuery(this).find(".item_jslist").hide();
            });
            
            //下拉框按钮事件
            jQuery(this).find(".item_btn").hover(function() {
                jQuery(this).removeClass('item_btnS').addClass('item_btnH');
            },function() {
                jQuery(this).removeClass('item_btnH').addClass('item_btnS');
            }).click(function() {
                var list = jQuery(item_me).find(".item_jslist");
                var csl=container.scrollLeft();//容器自身的left滚动值
                var cst=container.scrollTop();//容器自身的top滚动值
                var pe=container.offset(); //容器所在页面的坐标(top,left)
                var e = jQuery(this).offset(); //下拉按钮所在容器的坐标(top,left)
                
                //菜单所在位置 绝对定位 通过上边参数获得
                var l=e.left-pe.left+csl-83;
                var t=e.top-pe.top+cst+18;
                list.css("left", l);
                list.css("top",t);
                list.show();
            });
        }); //items.each(function(index,domEle){
        
        //下拉框相关事件
        var jslist=container.find(".item_jslist");
         jslist.children().hover(function() {
             // 鼠标移入改变背景
           jQuery(this).addClass('item_over');
           jQuery(this).children().filter(".btns").addClass("btnh");
        },function(){
              // 鼠标移入改变背景
             jQuery(this).children().filter(".btns").removeClass("btnh");
             jQuery(this).removeClass('item_over');
        });
        
        //下拉框被点击后隐藏
        jslist.children().click(function() {
            jslist.hide();
        });
        
        //可编辑列文本框样式
        items.find(".item_input").addClass("item_inputlabel");
        items.find(".item_input").focus(function(){
            jQuery(this).removeClass("item_inputlabel");
        });
        items.find(".item_input").blur(function(){
            jQuery(this).addClass("item_inputlabel");
        });
    }; //var InitItems=function(){
    
    //----------------------------行对象管理器 实例化参数(行对象的集合)----------------------------------------
    var ItemManager=function(_items){
        var itemsArray=_items;
        
        //传入itemID返回该ID对应的index
        this.index=function(ids){
            if(!ids && ids!==0) return '';
            var index='';
            ids+='';
            var idArray=ids.split(',');
            itemsArray.each(function(i){
                var id=jQuery(this).attr("itemID");
                for(var j=0;j<idArray.length;j++)
                {
                    if(id===idArray[j]) index+=i+','; 
                }
            });
            if(!index) return '';
            else return index.substr(0,index.length-1);
        };
        //传入index返回该index对应的itemID
        this.itemID=function(index){
            if(!index && index!==0) return '';
            var ids='';
            index+='';
            var iArray=index.split(',');
            itemsArray.each(function(i){
                var id=jQuery(this).attr("itemID");
                for(var j=0;j<iArray.length;j++)
                {
                    if(i.toString()===iArray[j]) ids+=id+',';
                }
            });
            if(!ids) return '';
            else return ids.substr(0,ids.length-1);
        };
        
        
        //获取本管理器下的OmItem的新实例
        this.getOmItem=function(selector){
            return new OmItem(itemsArray,selector);
        };
        
        //------特定OmItem类 可以绑定事件并将自己和该行的itemID作为参数传给事件的方法------
        var OmItem=function(items,selector){
            this.click=function(fn){
                this.bind('click',fn);
            }

            this.blur=function(fn){
                this.bind('blur',fn);
            }

            this.focus=function(fn){
                this.bind('focus',fn);
            }
            
            this.bind=function(type,fn){
                items.each(function(){
                    var me=jQuery(this);
                    var id = me.attr("itemID");
                    var target;
                    if(selector) target=jQuery(this).find(selector);
                    else target=jQuery(this);
                    target.each(function(){
                         var sender=jQuery(this);
                         sender.row=function(j){  //获取当前行中的其他item
                            if(j) return me.find(j);
                            else return me;
                         };
                         jQuery(this).bind(type,function(event){
                            sender.event=event;
                            fn(sender,id);
                         });
                    });// jQuery(this).find(items_i).each(function(){
                });//me.rows.each(function(){
            }; //this.bind
        };
    };
    
    //---------------------------------------------行选中管理器------------------------------------------------
    var RowSelectManager=function(_items){
        var items=_items;
        var _isOn=false; //是否启用行选中
        var ableCancel=false;//重复点击允许取消行选中
        var selectIndex=-1;  //行选中模式下的 被选中ID
        var onselect=new OmDelegate(); //行选中时执行的方法
        var oncancel=new OmDelegate(); //行取消选中时执行的方法
        
        //设置或获取重复点击允许取消行选中的状态
        this.cancel=function(_v){
            ableCancel=_v;
            return ableCancel;
        };
        
        //绑定基础行选中的方法
        items.each(function(i){
            jQuery(this).click(function(event){
                var obj = jQuery(event.srcElement ? event.srcElement : event.target);
                if(obj.is(".item_btn,.item_jslist *")) return;
                if(obj.is("input:not([select='true'])")) return;
                if(obj.is("[select='false']")) return;
                if(selectIndex!=i) //非同一行重复选中
                {
                    selectIndex=i; //保存当前选中行
                    onselect.run(i);
                }
                else if(ableCancel) //重复选中时，是否允许行取消
                {
                    selectIndex=-1;
                    oncancel.run(-1);
                }
            }); //jQuery(this).click(function(event){
        }); //items.each(function(i){
        
        
        //获取被选中的行或其中的元素
        this.selectedRow=function(selector){//行选中模式下 被选中的行
            if(!_isOn) return;
            var row;
            //eq不支持负数参数 所以做判断
            if(selectIndex==-1) row = items.slice(0,0); //返回一个空集合
            else row = items.eq(selectIndex);
            if(selector) return row.find(selector);
            else return row;
        }; 
        
        //获取被选中的行的index或者根据index选中指定行
        this.selectedIndex=function(index){
            if(!_isOn) return;
            if(index || index===0) items.eq(index).click();
            return selectIndex;
        };
        
        //绑定当发生行选中事件时执行的方法 _fn1选中时执行,_fn2取消选中时执行
        this.select=function(_fn1,_fn2){
            if(_fn1) onselect.add(_fn1);
            if(_fn2) oncancel.add(_fn2);
        };
        
        //设置行选中功能或获取当前是否开启行选中
        this.isOn=function(v){
            if(v) _isOn=v;
            else return _isOn;
        };
    };
    
    //-----------------------复选框管理器 实例化需2个参数(全选框对象,子复选框对象集)---------------------------
    var CheckBoxManager=function(a,i){
        var me2=this;
        var ckb_all=a;
        var ckb_item=i;
        var oncheck=new OmDelegate();
        
        //替换原全选复选框为新的三态复选框
        var newID="tri_chk_"+Math.floor(Math.random()*100000);
        var tsc="<div id='"+newID+"' class='ckb_div' status='none'></div>";
        ckb_all.hide().after(tsc); 
        
        var chkAll=jQuery("#"+newID); //新的三态复选框对象
        
        //全选复选框点击事件
        chkAll.click(function(){ 
            if(chkAll_Status()!="all") ckb_item.attr("checked", true);
            else ckb_item.attr("checked", false);// 取消选中
            ckb_all.click(); //执行原全选复选框的点击事件
            me2.check();
        });
        
        //设置或获取全选框的状态(all part none)并返回状态
        var chkAll_Status=function(v){
            if(v && chkAll.attr("status")!=v)
            {
                chkAll.removeClass("ckb_all").removeClass("ckb_part");
                if(v=="all" || v=="part")
                {
                   chkAll.addClass("ckb_"+v);
                   chkAll.attr("status",v);
                }else chkAll.attr("status","none");
            }
            return chkAll.attr("status");
        };
        
        //子复选框点击事事件
        ckb_item.click(function(){
            me2.check();
        });
        
        //----------------------CheckBoxManager类 对外暴露的事件----------------
        //设置或执行当复选框发生选中或取消选中时的事件，执行时检查全选框的状态
        this.check=function(fn){
            if(fn) oncheck.add(fn);
            else
            {
                var allLength=ckb_item.length;
                var ckLength=ckb_item.filter(":checked").length;
                if(allLength==ckLength) chkAll_Status("all");
                else if(ckLength==0) chkAll_Status("none");
                else chkAll_Status("part");
                oncheck.run();
            }
        };
        
        //设置或获取复选框管理器中复选框选中的索引值并返回索引值
        this.checkedIndex=function(i){
            if(i || i===0) //设置
            {
                i+='';
                var i_Array=i.split(',');
                ckb_item.removeAttr("checked");
                for(var j=0;j<i_Array.length;j++)
                {
                    ckb_item.eq(i_Array[j]).attr("checked",true);
                }
                me2.check();
                return i;
            }else //获取
            {
                var index='';
                ckb_item.each(function(i){
                    if(jQuery(this).filter(":checked").length>0) index+=i+',';
                });
                if(index.length==0) return index;
                else return index.substr(0,index.length-1);
            }
        };
    };//var CheckBoxManager=function(a,i){

    //-----------------------------------------OmGrid类 对外暴露可使用的公共方法--------------------------------------------
    // 初始化
    this.init=function(id) {
        ID(id);
        container=jQuery(_id);
        items=container.find(".item");
        title=container.find(".item_title");
        setGridContainer();
        unbindAll();  //清除所有方法
        InitItems();
        
        item_manager = new ItemManager(items);
        
        //行选中管理器
        select_manager = new RowSelectManager(items);
        
        //复选框管理器
        ckb_manager = new CheckBoxManager(title.find("[name='ckbAll']"),items.find("[name='ckbItem']"));
        
        //复选框值改变时，改变当前选中行的背景
        ckb_manager.check(function(){
            items.removeClass("item_cked");
            var index = ckb_manager.checkedIndex();
            if(index)
            {
                index+='';
                var array=index.split(',');
                for(var i=0;i<array.length;i++)
                {
                    items.eq(array[i]).addClass("item_cked");
                }
            }
        });
    };
    
    //查找Grid中的元素
    this.find=function(selector){
        return container.find(selector);
    };
    
    //查找title中的元素
    this.title=function(selector){
        return title.find(selector);
    };
    
    //获取行中元素返回OmItem对象
    this.item=function(v){
        if(v) return item_manager.getOmItem(v);
        else return items;
    };
    
    //获取或设置复选框选中的行 返回该行数据的ID
    this.checkedID=function(ids){
        if(ids || ids===0)
        {
            var index = item_manager.index(ids);
            ckb_manager.checkedIndex(index);
        }
        var index = ckb_manager.checkedIndex();
        return item_manager.itemID(index);
    };
    
    //获取复选框选中的行或其中的子元素(selector筛选)
    this.checkedRow=function(selector){
        if(selector)return ckb_manager.checkedRow(selector);
        else return ckb_manager.checkedRow();
    };
    
    //获取或设置行点击选中的行 返回该行数据的ID
    this.selectedID=function(id){
        if(!isNaN(id))
        {
            var index = item_manager.index(id);
            select_manager.selectedIndex(index);
        }
        var index = select_manager.selectedIndex();
        return item_manager.itemID(index);
    };
    
    //获取或设置行点击选中的行 返回该行数据的ID
    this.selectedRow=function(selector){
        if(selector) return select_manager.selectedRow(selector);
        else return select_manager.selectedRow();
    };
    
    //开启行选中功能
    this.select=function(arg1,arg2,arg3){
        var _fnOnSelect,_fnOnCancel;
        if(typeof arg1 == "boolean")
        {
            select_manager.cancel(arg1);
            _fnOnSelect=arg2;
            _fnOnCancel=arg3;
        }else
        {
            _fnOnSelect=arg1;
            _fnOnCancel=arg2;
        }
        if(!select_manager.isOn())
        {
            select_manager.isOn(true);
            //行选中时执行的默认样式方法
            select_manager.select(function(index){
                items.removeClass('item_cked');
                items.eq(index).addClass("item_cked");
            },function(){
                items.removeClass('item_cked');
            });
        }
        if(_fnOnSelect)
        {
            if(_fnOnCancel) select_manager.select(_fnOnSelect,_fnOnCancel);
            else select_manager.select(_fnOnSelect,_fnOnSelect);
        }
    };
}


//支持2个参数的委托类
var OmDelegate = function() {    
    var ArrayFuction = [];   
    //添加方法，相当于+=   
    this.add = function(f) {   
        if (typeof (f) === "function") ArrayFuction.push(f);     
        else throw new Error("委托只能接受方法");   
    };   
    //移除方法，相当于-=   
    this.remove = function(f) {   
        if (typeof (f) === "function") {   
            var length = ArrayFuction.length;   
            for (var i = 0; i < length; i++) {   
                if (ArrayFuction[i] == f) ArrayFuction = ArrayFuction.splice(i, 1);   
            }   
        }   
        else throw new Error("委托只能接受方法");    
    };   
    //调用委托下所有方法   
    this.run = function(arg1,arg2) {
        for (var item in ArrayFuction) {
            ArrayFuction[item](arg1,arg2);   
        }   
       
    }
}   