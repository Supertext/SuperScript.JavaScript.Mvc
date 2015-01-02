_**IMPORTANT NOTE:**_ This project is currently in beta and the documentation is currently incomplete. Please bear with us while the documentation is being written.

####SuperScript offers a means of declaring assets in one part of a .NET web solution and have them emitted somewhere else.


When developing web solutions, assets such as JavaScript declarations or HTML templates are frequently written in a location that differs from their desired output location.

For example, all JavaScript declarations should ideally be emitted together just before the HTML document is closed. And if caching is preferred then these declarations should be in an external file with caching headers set.

This is the functionality offered by SuperScript.



##The Script Container Block

This project contains one publicly-accessible class, `SuperScript.JavaScript.Mvc.Containers.HtmlExtensions`,
which contains one overloaded method, `JavaScriptContainer()`. This method can be used on .NET Razor views for encapsulating a 
block of JavaScript.

For example

```HTML
@using SuperScript.JavaScript.Mvc.Containers
...
@using (Html.JavaScriptContainer())
{
    <script type="text/javascript">

        var myVar = "hello world";

    </script>
}
```

The overloads permit specifying
* the emitter key
* an index in the application-wide collection of `SuperScript.Declarables.DeclarationBase` objects at which the script in this block should be inserted

##Dependencies
There are a variety of SuperScript projects, some being dependent upon others.

* [`SuperScript.Common`](https://github.com/Supertext/SuperScript.Common)

  This library contains the core classes which facilitate all other SuperScript modules but which won't produce any meaningful output on its own.

* [`SuperScript.Container.Mvc`](https://github.com/Supertext/SuperScript.Container.Mvc)

  This project does not wholly offer the functionality for working with, for example, JavaScript or HTML templates on Razor views. Rather, it offers the base functionality required by the related projects `SuperScript.JavaScript.Mvc` and `SuperScript.JavaScript.WebForms`.

* [`SuperScript.JavaScript`](https://github.com/Supertext/SuperScript.JavaScript)

  This library contains functionality for making JavaScript-specific declarations such as variables or function calls.

`SuperScript.JavScript.Mvc` has been made available under the [MIT License](https://github.com/Supertext/SuperScript.JavaScript.Mvc/blob/master/LICENSE).
