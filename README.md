# 💀 덱 빌딩 로그라이크 (임시 제목)

---

## 📂 프로젝트 스크립트 구조

┣ 📂 Claw                	# 뽑기 기계 관련 스크립트

 ┃ ┣ 📜 ClawGamePhysics.cs	#

 ┃ ┣ 📜 ClawPhysics 		#

 ┃ ┣ 📜 ClawSplinePhysics 		#

 ┃ ┣ 📜 ContainerPhysics 		#

 ┃ ┣ 📜 LIneFollowing 		#
 

┣ 📂 Common                 # 공용 스크립트

 ┃ ┣ 📜 CanvasBase.cs 		#

 ┃ ┣ 📜 Enum.cs 			#

 ┃ ┣ 📜 ResourceController.cs 	#

 ┃ ┣ 📜 Singleton.cs		#

 ┃ ┣ 📜 StatHandler.cs 		#

 ┃ ┣ 📜 UIBase.cs 		#


┣ 📂 Editor                # ??

 ┃ ┣ 📜 ItemSlotEditor.cs 		#
 

┣ 📂 Enemy                   	# 적 관련 스크립트

 ┃ ┣ 📜 Enemy.cs 		#
 

┣ 📂 Group                	# 열거체, 인터페이스 선언용

 ┃ ┣ 📜 EnumGroup.cs 		#

 ┃ ┣ 📜 InterfaceGroup.cs 		#

 
┣ 📂 Item            	# 아이템 관련 스크립트

 ┃ ┣ 📜 InventoryItem.cs 		#

 ┃ ┣ 📜 Item_HealPotion.cs	#

 ┃ ┣ 📜 Item_Shield.cs 		#

 ┃ ┣ 📜 Item_Sword.cs 		#

 ┃ ┣ 📜 ItemObject.cs 		#

 ┃ ┣ 📜 ItemSpawner.cs 		#

 ┃ ┣ 📜 Skill.cs 			#

 ┃ ┣ 📜 TempItem.cs		#

 
┣ 📂 Manager              	# 매니저 관련 스크립트

 ┃ ┣ 📜 AudioManager.cs 		#

 ┃ ┣ 📜 EnemyManager.cs 		#

 ┃ ┣ 📜 GameManager.cs 		#

 ┃ ┣ 📜 ItemInventoryManager.cs 	#

 ┃ ┣ 📜 ResourceManager.cs	#

 ┃ ┣ 📜 TurnManager.cs 		#

 ┃ ┣ 📜 UiManager.cs 		#


┣ 📂 Player                  	# 플레이어 관련 스크립트

 ┃ ┣ 📜 Character.cs 		#

 ┃ ┣ 📜 Player.cs 			#

 ┃ ┣ 📜 PlayerAnimationEvents.cs 	#


┣ 📂 Scriptable              	# 스크립터블 오브젝트 관련 스크립트

 ┃ ┣ 📜 ItemSO.cs 		#

 ┃ ┣ 📜 ShopItem.cs 		#

 ┃ ┣ 📜 SkillSO 			#

 ┃ ┣ 📜 StatData 		#

 
┣ 📂 Skill                       # ??

 ┃ ┣ 📜 Skill_PowerUP.cs 		#


┣ 📂 StateMachine        	# 플레이어 상태 머신 관련 스크립트

 ┃ ┣ 📜 BaseState.cs 		#

 ┃ ┣ 📜 BaseStateMachine.cs 	#

 ┃ ┣ 📜 PlayerStateMachine.cs	#

 ┃ ┣ 📂 PlayerStates

 ┃ ┃ ┣ 📜 PlayerBattleState.cs 	#

 ┃ ┃ ┣ 📜 PlayerIdleState.cs	#

┣ 📂 UI                	# UI  관련 스크립트

 ┃ ┣ 📜 CanvasSample.cs		#

 ┃ ┣ 📜 CanvasShop.cs		#

 ┃ ┣ 📜 Card.cs			#

 ┃ ┣ 📜 CardStage.cs		#

 ┃ ┣ 📜 CharacterStatUI.cs		#

 ┃ ┣ 📜 FadeInOut.cs		#

 ┃ ┣ 📜 Floor.cs			#

 ┃ ┣ 📜 GoldUI.cs		#

 ┃ ┣ 📜 HpBar.cs			#

 ┃ ┣ 📜 IndicatorUI.cs		#

 ┃ ┣ 📜 ItemSlot.cs		#

 ┃ ┣ 📜 NotificationManager.cs	#

 ┃ ┣ 📜 NotoficationPool.cs	#

 ┃ ┣ 📜 PausePopup.cs		#

 ┃ ┣ 📜 StageClearPanel.cs		#

 ┃ ┣ 📜 UIGameOver.cs		#

 ┃ ┣ 📜 UIMain.cs		#

 ┃ ┣ 📜 UIReward.cs		#

 ┃ ┣ 📜 UIShop.cs		#

 ┃ ┣ 📜 UIStageShow.cs		#

 ┃ ┣ 📜 UIStart.cs		#

 ┃ ┣ 📜 UITop.cs			#

---

## 🎮 인게임 구성

뽑기 시스템 제작 - 박성준

---

## ⚙ 주요 시스템

**조작법**
- 

**적**
- 

**자원**
- 파란 보석 - 기본 자원으로 건축에 사용
- 보라 보석 - 기본 자원으로 건축에 사용
- 빨간 보석 - 희귀 자원으로 드레곤 포탑 건축에 사용

---

## 🎥 플레이 영상
