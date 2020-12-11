# raygamecsharp - sample project

This is a sample C# project setup with [raylib][raylib] for Visual Studio.
Raylib is a simple game programming framework that is designed to be friendly to
beginners. It is created by [Ramon Santamaria (@raysan5)][raysan].

The language binding is maintained at [ChrisDill/Raylib-cs][rl-cs-bindings] and
is compatible with Raylib 3.0.

It is primarily intended for use by students in the Game Programming course at
the Seattle Campus of the Academy of Interactive Entertainment.

[raylib]:https://github.com/raysan5/raylib
[raysan]:https://github.com/raysan5

## Getting Started

To get started with this sample project, click the _Use this template_ button
up top to copy this repository to your own account. Then, follow the
instructions in the [Building](#building) section below.

Once you've got it working, check out the bindings file from Raylib-cs to learn
about the C# API for Raylib or browse the example projects rewritten in C#.

**raylib links** - [website][rl-website] / [discord][rl-discord] / [reddit][rl-reddit] / [raylib repo][rl-repo] / [c\# bindings repo][rl-cs-bindings], [api reference][rl-cs-bindings-ref], [examples][rl-cs-examples]

[rl-website]:https://www.raylib.com/
[rl-discord]:https://discord.gg/VkzNHUE
[rl-reddit]:https://www.reddit.com/r/raylib/
[rl-repo]:https://github.com/raysan5/raylib
[rl-cs-bindings]:https://github.com/ChrisDill/Raylib-cs
[rl-cs-bindings-ref]:https://github.com/ChrisDill/Raylib-cs/blob/master/Raylib-cs/Raylib.cs
[rl-cs-examples]:https://github.com/ChrisDill/Raylib-cs-Examples

## Building

Building this project requires **Visual Studio 2019** or newer.

> This is written against .NET Core 3.1 and primarily supports Windows.
> Adjustments may be needed for other platforms.

Clone the repository and open the solution in Visual Studio. Both the solution
and project should already be configured and ready to start working with. To
test this, build and run the provided sample project.

![A screenshot of the included sample project](.github/raygame.png)

The sample project that is provided is the [basic window example][basicexample]
from raylib-cs. Further examples can be found in its [repository][rl-cs-examples].

A full list of all methods and types made available via the [binding can be
found in the Raylib-cs repository][rl-cs-bindings-ref].

[basicexample]:https://github.com/ChrisDill/Raylib-cs-Examples/blob/master/Examples/core/core_basic_window.cs

## License

MIT License - Copyright (c) 2019-2020 Academy of Interactive Entertainment

For more information, see the [license][lic] file.

Third party works are attributed under [thirdparty.md][3p].

[lic]:LICENSE.md
[3p]:THIRDPARTY.md
[raylib]:https://github.com/raysan5/raylib
