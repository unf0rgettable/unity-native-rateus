# Native Rate Us

Модуль предназначен для простого интегрирования в проект нативного вызова rate us (app review) окна.

# Dependencies

Добавьте следующие зависимости в manifest.json :

```JSON
  "com.littlebitgames.coremodule": "https://github.com/LittleBitOrganization/evolution-engine-core.git",
  "com.unfo.ratemodule": "https://github.com/unf0rgettable/unity-native-rateus.git"
```

# Quick Start

Пример интеграции в проект с использованием Zenject. Для вызова корутины используется ICoroutineRunner из core модуля.

```c#
        public override void InstallBindings()
        {
            Container
                .Bind<RateUsService>()
                .AsSingle()
                .NonLazy();
                
            Container
                .Bind<ICoroutineRunner>()
                .FromInstance(this)
                .AsSingle()
                .NonLazy();
        }
```