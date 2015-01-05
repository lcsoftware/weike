// 注册命名空间Grandsoft.GEA, Grandsoft.GCM
Namespace.register("Grandsoft.GEA");


// 在Grandsoft.GEA命名空间里面声明类Person
Grandsoft.GEA.Person = function (name, age) {
    this.name = name;
    this.age = age;
}
//userName, password, confirm 

Grandsoft.GEA.User = function (username, password, confirm) {
    this.username = username;
    this.password = password;
    this.confirm = confirm;
}

// 给类Person添加一个公共方法show()
Grandsoft.GEA.Person.prototype.show = function () {
    alert(this.name + " is " + this.age + " years old!");
}

// 演示如何使用类Person
var p = new Grandsoft.GEA.Person("yanglf", 25);


//p.show();