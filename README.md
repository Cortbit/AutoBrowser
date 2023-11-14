# AutoBrowser
自动脚本浏览器工具，自定义脚本，网际巡航，页游辅助

## 初版实现 cs 脚本动态编译
### 内置基本函数：
* MsgBox
 ```
int MsgBox(object msg[, object title[, int timeout]]);
 ```
* InputBox
 ```
string InputBox(object title, string defaultValue, object description[, int timeout]);
 ```
* GetColor/GetPosColor
```
System.Drawing.Color GetColor(int x, int y); //| 判断 PointMode 
```
```
System.Drawing.Color GetPosColor(int x, int y); //| 全局坐标
```
* FindTarget
```
System.Drawing.Point[] FindTarget(string name);
```
* Sleep
```
void Sleep(int times); //| 脚本执行中等待(ms)
```
* MouseMove
```
void MouseMove(int x, int y); //| 移动鼠标到指定位置，判断 PointMode
```
* MouseDown
```
void MouseDown(string button); //| 模拟按下鼠标某键, "Left","Right","Middle"
```
* MouseUp
```
void MouseUp(string button); //| 模拟抬起鼠标某键, "Left","Right","Middle"
```
* KeyDown
```
void KeyDown(string key); //| 模拟按下键盘某键, KeyDown("A"); KeyDown("Alt");
```
* KeyUp
```
void KeyUp(string key); //| 模拟按抬起键盘某键, KeyUp("A"); KeyUp("Alt");
```
* GetPointMode/SetPointMode
  *获取和设置 PointMode [global/local]* 
* LikeColor/LikeColorValue
   *颜色模糊比较*
---
![主界面](./P1.jpg)
