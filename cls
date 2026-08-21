[33m47098ed[m[33m ([m[1;36mHEAD[m[33m -> [m[1;32mfeature/Attack[m[33m, [m[1;31morigin/feature/Attack[m[33m)[m take damage with enemy
[33mb6e59a9[m Attack Point 依據移動方向變化
[33m8bc2882[m can hit to enemy
[33m9e2808a[m 分類 and 調整參數
[33m7dbf8bc[m optimization coroutine functions
[33m782c84b[m 三聯擊 & timeout
[33m5a7b108[m redefine method name and function
[33ma92000d[m 更改排列順序
[33m574b81a[m Attack Cooldown
[33ma490c7f[m definition some field nouns
[33m30a2d42[m create Menu script
[33m416f022[m use 'else if' with all state transition
[33m15fec8c[m Attack transition - Idle & Move -> Attack Normal - Attack Normal -> Idle & Move
[33mfd36dd2[m remove can attack optional
[33m9fb1042[m Set Animation Name
[33m127f78c[m Create Attack States and Organiz State Datas
[33mb566da1[m Player Attack Animation - rename animation name: Up->top, Down->Bottom - Play Animation with attack input
[33m8742325[m[33m ([m[1;31morigin/develop[m[33m, [m[1;32mdevelop[m[33m)[m Organize ScriptableObject Data Menu
[33m9b9ada9[m Indentation CreateAssetMenu with PlayerState_Move
[33m3d335cc[m Exit Guard Break State Method
[33m7b29a2e[m change Guard Break transition of logics
[33m6cb83be[m cinemachine Damping changed
[33m375e053[m transition of Guard Break States and Normal States
[33m40a9aca[m GuardBreak_Idle and GuardBreak_Move
[33mbd0b01c[m 需要修改 - 改為 PlayerState_GuardBreak_Idle, Move
[33me457ece[m Guard Break move faces
[33mbe87a86[m change logic of move and dependency
[33m7526b78[m changed move properties - direction - animation parameter
[33m6ef7b2a[m MoveX and MoveY to PlayerMove Script
[33m418c66d[m PlayerState rename CanAttack => CanBe_Attack - defult value to true
[33m581ad8e[m 稍微修改 - 移入新資料夾 - 稍微修改 PlayerPosture Script
[33meea16e9[m hide field in inspector
[33m386da2f[m organize the context
[33m577c47c[m add can be Attack delegate in PlayerState - if can be attack and input attack button, then print that
[33m13ceb0a[m in PlayerState init all context
[33mbf1555b[m add something attack - create new attack button down input - simple PlayerCombat script
[33m367753e[m posture fill change to blue
[33mc0e4bb8[m Camera Confiner
[33m9325bdf[m create tilemap of fences collider
[33m00a1d8b[m change currPosture bias
[33m45101df[m 依據 TimeGap 倒數完, 減少 Posture  Dash 的時候重新倒數
[33mcef11d5[m Rename Observer Pattern element
[33m46f2b5d[m Posture 連續性下降
[33m785d174[m Dash Posture Value change to PlayerDash Script
[33m2d753df[m 修改一些東西
[33ma0d210c[m Initialize Context to reuse
[33m81d4667[m GuardBreak 邏輯大致完成 有些地方需調整
[33mbcfb44c[m create GuardBreak state - 需要再調整
[33m6b1f6db[m set field with Inspector
[33m94e9cfd[m take posture
[33mab02373[m set fills
[33mba28fee[m add new script and Organize posture folders
[33m1094ba9[m switch of updated
[33m99bca6f[m develop merge with feature/PlayerStateMachine
[33m109dde7[m Change StateMachine System of Get Component Methods
[33m0448a59[m merge
[33mdd0c8d9[m add
[33m3313708[m Change Animation Name Express
[33maadb803[m 調整 Dash 參數
[33mbf32618[m Player Dash
[33mc553639[m Can Be Rename XY Move Input Name
[33m81ca003[m Player Move Direction
[33mf40ecb5[m animation with hash
[33m11df32a[m add CineMachine and Flip function
[33mfb7c59e[m Player Move
[33m5c1e94a[m Player Input
[33m09c5cf1[m Import 'Input System' and add Input Actions
[33m2642001[m change animator motion to Blend Tree
[33m719807b[m Add Animator with Player Animation
[33med119f4[m Rename PlayerState - GameManager of PlayerState -> Player_State - this of Player_State -> PlayerState
[33mbea8de7[m PlayerState 優化
[33m8a1ac0f[m 最基礎狀態機 - convert -> Player State - 創建好最基礎架構
[33m624a4cb[m Merge branch 'feature/Panel' into develop
[33md0755e2[m[33m ([m[1;31morigin/feature/Panel[m[33m, [m[1;32mfeature/Panel[m[33m)[m navigation Selection finish
[33m718f0a6[m 重新整理架構
[33md4eb3c4[m MainMenu 大致架構 需要補充細節
[33m8ae9a3e[m Conbineed with 'MainMenuButtonEffect'
[33m0773ffc[m creat basic state machine
[33medd2733[m add forders
[33m890d204[m MainMenu Navigation Selection - 滑鼠點擊 canvas 不會跳掉
[33m490ce6b[m options menu cross button
[33mb994244[m 小變更
[33mcb11784[m Menu Button 鍵盤選取時放大倍率
[33m94b2a80[m change Menus arrangement - Options Menu into Canvas-Menus - Change Options Buttons of OnClick methods
[33m9d5383e[m change TMP outline
[33mde32b95[m change title and button text color change to white
[33mec97620[m 於 Asset 新增 KeyArt Image
[33m7f1ed6c[m Merge branch 'feature/PlayerHealthBar' into develop
[33m2da7f74[m For test
[33m3fe0e8b[m test for health bar - click right mouse button to take damage
[33mf1511fa[m update posture bar
[33m5dc2ba9[m change asset position Posture-Fill-Rect
[33m2eb74e7[m Merge branch 'feature/PlayerPostureBar' into develop
[33m277955f[m debug Posture Bar can not execute err
[33mad86639[m Merge branch 'feature/PlayerPostureBar' into develop
[33m17494fa[m delete old posture bar
[33ma27c54f[m Place Titles into MainMenu
[33m4024e03[m Change buttons layout method
[33mcd0b0a4[m Change Text Font - Legacy -> TMP - Title - Subtitle - Main Menu Buttons
[33mc62fbc3[m Simple Keyboard Navigation
[33m0b50700[m Main Menu - rename Scene - Start Game - Quit Game
[33maa90481[m change check square/mark
[33ma088cd9[m Panel Screen Full Screen - 外觀
[33ma54078c[m Panel Audio/Screen can be switched
[33m641f209[m Options Panel - click on Options button -> Hide KeyArt - click on Options Panel cross button -> Show KeyArt
[33m44a1f57[m Options Panel - Component Rename -> UI - Cross Button with Option Panel
[33m9dbaede[m KeyArt 主頁面 - Buttons -> MainMenu - Click option button to pop up the Option Panel
[33m021507b[m the Panel of Audio UI - Captions Texts
[33m9d5232a[m the Panel of Audio UI - 3 Slider placed
[33m7b88bfd[m rename forder
[33m50d73fb[m Change Panel Image
[33m3d57482[m Options-Panels - Panel - Button - appearance
[33m805e6cf[m organize folders 2
[33m920bd20[m organize folders
[33mf31db28[m Key Art - Button - Initial appearance compelete
[33mb0a398f[m Key Art - Title - Subtitle
[33m77b1090[m 新增UI素材
[33m031007b[m Place Fill Area
[33ma6bcf4d[m Place Fill Area
[33m2d89fef[m Posture Bar 基本外觀與位置
[33m3ad07f0[m HealthBar 功能建立
[33m1910908[m HealthBar - 引入圖片素材 - 建立外觀 - 位置擺放 - Slider Component
[33ma4cc643[m[33m ([m[1;31morigin/main[m[33m, [m[1;31morigin/HEAD[m[33m, [m[1;32mmain[m[33m)[m some update
[33m5e4f362[m clone project and Player Fall Programs
[33m0e04e81[m Rebuild the overall architecture forder
[33mf326fc4[m debug
[33m3640194[m Replace Attack Animation Frames
[33m2273bce[m append Self Attack Animation
[33m8022e14[m Replace Dash Hori Animation Frame
[33mdddc90e[m Apply UI - Font - Button - GameOver Panel
[33md7ad6ac[m Update Animation about Player - Idle - Move - Dash
[33mdf8bfbb[m remove enemy when it Dead
[33ma7ec65e[m Add most of the sound effects
[33mcdec90d[m rename with game name
[33m9535ae6[m Change some params
[33md232267[m remove attack of posture damage
[33mc49419b[m Posture 相關系統導入 Eenmy
[33m6879684[m Down Animation
[33mb2db06a[m add RetryGame method
[33m36b533f[m append Down trigger parameter
[33m8aa1b03[m Enemy Attack Animation need to Debug
[33m6494c76[m Enemy 往左移動時轉向
[33m129d9fd[m Enemy 大致動畫導入成功
[33maddb10f[m Change HitStun Rule 1. Posture 歸零 2. 改為播放 Idle 動畫
[33m7f37779[m remove GuardBreakDuration
[33mcaa8ffb[m Guaed break 狀態下 posture 下降速率獨立
[33m5cf8d78[m 使用百分比方式控制 GuardBreak
[33mc2decb0[m HitStun prototype
[33m6d4c73a[m 保留 Attack 於 Guard Break 期間
[33me83c88d[m 解決 posture 無窮增長問題
[33m8f77be4[m 修復 Dash posture 增加不平均問題
[33m750c6f8[m In MoveController use Speed repace _speed
[33m1d549c9[m In MoveController use Speed repace _speed
[33m92bc0cf[m revised can not dash bug
[33mace343e[m Guard Break Prototype In Guard Break State to move slowly
[33m6edfdfd[m remove something
[33ma6fc37b[m sloved recovery to 0 problem with Posture
[33m7cb1361[m rename and change code position rename Posture Method of 'DelayedReset'
[33md14ef96[m remove StateMachine classification change state function has been moved to each class
[33m6c45ece[m StateMachine with EnemyIndepentent
[33mdaaad19[m change slowReset to Adjustable
[33m108d14d[m SlowReset complete
[33m1752cf6[m remove hurt state
[33m2963890[m sloved attack bug
[33m730224f[m need to debug with player attack enemy
[33m6fe3f27[m posture slowly growth
[33m5d980b3[m GameManager
[33ma2dc8e7[m Attack reduction posture
[33mf8b1ad9[m add some Access Modifiers
[33ma2adec7[m Add EnemyAttack Script
[33m1b3396d[m Menu and Scene
[33mc10034c[m Inital Game Directory
[33m615e228[m sloved with dash speed not same problem
[33m22b5bbb[m Game InGame State with Player and Enemy state
[33m8513f20[m GmaeManager control Player's Actions
[33mec5f0d7[m add UI files
[33m8b54a3b[m add Timer Machine
[33m6b7201a[m Posture Changed
[33m3c05a92[m in attack range, wait and attack
[33m22662dd[m Enemy attack prototype model
[33mf67cb85[m Dash 期間播放動畫(以Attack 動畫測試)
[33mdd0bf08[m AI 整理程式碼
[33m95e8242[m 優化 DashController
[33ma2d8fac[m add Attack with EnemyController
[33ma691123[m enemy Animation 1. creat empty enemy animation 2. edit enemy sprite sheet 3. rename sprite sheet actions
[33m6d2e2d1[m 調整 1. Movement rename to MoveController 2. Organize folders with 'Action' 3. 調整與移除不必要的 field
[33m3611c09[m hurt state prototype 1. player posture prototype 2. player health prototype
[33mf1412fa[m Movement 1. 解決移動穿牆問題 2. 解決Dash無法移動問題
[33md7bd2de[m Use DashData
[33m8782f88[m PostureBar Prototype
[33m1ac8d71[m Hidden cursor
[33m9b193ce[m save
[33m9006460[m about movement 1. rename with MoveController to Movement 2. creat a subclass EnemyMovement with Movement 3. override 'Move' Method in EnemyMovement
[33mb802ffd[m sloved with Player twitch problem
[33mb210f76[m Inputhandler
[33m78192c3[m Attack Animation when can not attack, animator be transition to idle state need to debug
[33m0e09fbe[m change animator 1. use blend tree instead of traditional animator 2. can not paly animation with attack
[33m589a421[m Idle direction
[33mb3e64ed[m Animation change 1. add and adjustment some animation 2. append move Up and Down on Controller
[33m37a7382[m can attack determination
[33m65e5333[m About AttackController 1. add AttackData to manage 2. CoolDown
[33m3d18350[m Attack Range Direction Logics
[33m48a33fd[m change attack parametor position
[33m6264547[m Attack with rate
[33mc979219[m change some SpriteSheet names
[33m9f8d3b2[m AttackController Prototype
[33mec6757f[m MoveInput change and append some properties
[33mdcb6e03[m 降低動畫機耦合度
[33mbea04bf[m 左右換方向
[33mc00e928[m add script with attack
[33m4d04b2f[m Forder renmae
[33m43f583b[m Attack Animations 1. Animator and Animations 2. InputController 3. StateMachine
[33m72eae38[m Health Bar 測試成功
[33md989a12[m The general logic of HealthBar
[33m77d5de4[m HealthBar Border
[33ma2f3903[m Player Animations
[33m2aa9e2e[m Animations
[33m9af8fcb[m change movePressed logic GetButtonDown and GetButtonUp
[33me53c6f4[m change to GetKey 解決無故停止問題
[33m3f7603f[m rename with Player SpriteSheet
[33mc6b1018[m 物理機制更新 1. switch case StateMachine 解耦為 PlayerActionState, MoveState 2. MoveState 放置於 FixedUpdate 3. Update 接收 Move keyword 輸入
[33m6f69d50[m 配合物理引擎更改 DashRoutine
[33mecb57be[m 解決穿牆問題
[33ma2e6d30[m remove InputController with DashController
[33m2545a56[m Optimize InputController use Unity InputManager to get direction
[33mce8fc44[m use InputManager creat new vector
[33mda0183d[m 引入素材 1. 處理素材做成 Tile palette & Tilemap 2. 使用 Tilemap
[33m44a416c[m Optimize MoveController
[33m175eccc[m Enemy Chase logic
[33mf401d8f[m SetTarget
[33m568f660[m rename StateMachine properties
[33m869d4c2[m Decoupling of EnemyController to DetectionRange
[33mc452928[m Enemy of trigger test
[33mbb53d30[m change forder
[33m88edcff[m change CameraFollow Scripts
[33mc7082f5[m Camera Confiner
[33md81503f[m append camera follow scripts
[33mcd9b2ac[m organize the architecture
[33meea4cea[m dash was complete
[33m82e3249[m dash problem was sloved
[33m2147a17[m 可顯示 dash 狀態，但依b06
[33m027bf0d[m change dash pressed logic
[33m77fb865[m no issues that state of move and idle
[33md30e7e1[m change achitecture of move direction
[33m37d986a[m 解決方向單一問題
[33mad64dbf[m instant InputController
[33m553e692[m same
[33m31b4759[m change MoveController Architecture
[33mca8cdc6[m use StateMachine for player's Dash
[33me239f09[m Dash input change to 'Space'
[33m1850f03[m add new forder ControllerModules
[33m71e760a[m use InputController for manage dash input
[33m8c18cc5[m can be dash
[33m4b3c029[m can move
[33m9628fa1[m MoveController replaces 'Move' Method
[33mae445cb[m add MoveController Script
[33maba84f6[m new branch
[33m5c7d9ac[m private of verical and horizontal
[33m692e8a9[m List all state
[33m41adfce[m GetComponent of DashController
[33mdb7cb95[m use GetComponent to get rb
[33m43380a3[m use GetComponent to get InputController
[33me3d9141[m reset fsm
[33m290022f[m fsm control dash need to debug
[33mf554865[m Dash rewriting
[33m3c6ab66[m Instance of Field
[33m312bee4[m remove trail
[33mebce08b[m dash of direction
[33m6c8f1ea[m Dash prototype
[33m072ba59[m use #regin to Manage Programs 1. Unity LifeCycle 2. Self Methods
[33m1d174ab[m Player movement press vecticle and horizontal, don't dash
[33m8c9f626[m 使用 Unity Input Manager 控制玩家移動
[33m18f924c[m 暫時改為Axis方式取得Input
[33mea3e323[m Basic movement script
[33medc81d6[m 引入暫時Player素材
[33mb377762[m tile map 1. 引入 Asset Store 資源 2. 鋪蓋初始 Tilemap
[33m67de5ef[m init unity project and add gitignore
[33m4e256ae[m first commit
