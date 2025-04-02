# 던전 레이더스

---

## 📂 프로젝트 스크립트 구조

┣ 📂 Claw                	# 뽑기 기계 관련 스크립트 - 박성준
 ┃ ┣ 📜 ClawGamePhysics.cs	# 뽑기 기계 다른 스크립트 참조용.

 ┃ ┣ 📜 ClawPhysics 		# 뽑기 기계 움직임

 ┃ ┣ 📜 ClawSplinePhysics 		# 뽑기 기계 배출 시스템

 ┃ ┣ 📜 ContainerPhysics 		# 뽑기 기계 순차적으로 내려오도록 만든 시스템

 ┃ ┣ 📜 LIneFollowing 		# 라인 렌더러 관련

┃ ┣ 📂 OldVersion	#구버전임(상관X)


┣ 📂 Common                 # 공용 스크립트 

 ┃ ┣ 📜 CanvasBase.cs 		# 캔버스 베이스 - 박성주

 ┃ ┣ 📜 ResourceController.cs 	# 플레이어, 적 자원 및 스탯 관리 - 배연두

 ┃ ┣ 📜 Singleton.cs		# 싱글턴 스크립트 - 박성주

 ┃ ┣ 📜 StatHandler.cs 		# 스탯 조정 - 배연두

 ┃ ┣ 📜 UIBase.cs 		# UI 베이스 스크립트 - 박성주


┣ 📂 Editor                # 커스텀 에디터

 ┃ ┣ 📜 ItemSlotEditor.cs 		# 아이템 슬롯 관련 커스텀 인스펙터 - 박성주
 

┣ 📂 Enemy                   	# 적 관련 스크립트

 ┃ ┣ 📜 Enemy.cs 		# 적 행동 스크립트 - 배연두
 

┣ 📂 Group                	# 열거체, 인터페이스 선언용

 ┃ ┣ 📜 EnumGroup.cs 		# 열거체 정리용

 ┃ ┣ 📜 InterfaceGroup.cs 		# 인터페이스 정리용

 
┣ 📂 Item            	# 아이템 관련 스크립트

 ┃ ┣ 📜 DestroyZone.cs 		# 아이템 청소용

 ┃ ┣ 📜 InventoryItem.cs 		# UI용 인벤토리 아이템 - 배리안

 ┃ ┣ 📜 Item_Axe..cs		# 도끼 아이템

 ┃ ┣ 📜 Item_HealPotion.cs	# 힐링 아이템

 ┃ ┣ 📜 Item_Shield.cs 		# 방패 아이템

 ┃ ┣ 📜 Item_Sword.cs 		# 검 아이템

 ┃ ┣ 📜 ItemObject.cs 		# 아이템 정보를 담고있는 클래스

 ┃ ┣ 📜 ItemSpawner.cs 		# 아이템 스폰

 
┣ 📂 Manager              	# 매니저 관련 스크립트

 ┃ ┣ 📜 AudioManager.cs 		# 오디오 매니저

 ┃ ┣ 📜 EnemyManager.cs 		# 에너미 매니저

 ┃ ┣ 📜 GameManager.cs 		# 게임 매니저

 ┃ ┣ 📜 ItemInventoryManager.cs 	# 아이템 인벤토리 매니저

 ┃ ┣ 📜 ResourceManager.cs	# 에셋 끌어오는 매니저

 ┃ ┣ 📜 StageManager.cs		# 스테이지 매니저

 ┃ ┣ 📜 TurnManager.cs 		# 턴제 관리 매니저

 ┃ ┣ 📜 UiManager.cs 		# UI 매니저


┣ 📂 Player                  	# 플레이어 관련 스크립트

 ┃ ┣ 📜 Character.cs 		# 캐릭터 스크립트 참조용

 ┃ ┣ 📜 Player.cs 			# 플레이어 스크립트

 ┃ ┣ 📜 PlayerAnimationEvents.cs 	# 플레이어 애니메이션 관리

 ┃ ┣ 📜 TurnLimitStat.cs 		# 버프 지속 턴 차감용


┣ 📂 Scriptable              	# 스크립터블 오브젝트 관련 스크립트

 ┃ ┣ 📜 ItemSO.cs 		# 아이템

 ┃ ┣ 📜 ShopItem.cs 		# 상점아이템

 ┃ ┣ 📜 SkillSO 			# 스킬 아이템

 ┃ ┣ 📜 StatData 		# 스킬 데이터

┃ ┣ 📂 PlayerStates

 ┃ ┃ ┣ 📜 StageData.cs		# 스테이지 데이터

 ┃ ┃ ┣ 📜 StageItem.cs		# 스테이지 아이템

 
┣ 📂 Skill                       # 스킬 관련 스크립트

 ┃ ┣ 📜 Skill_PowerUP.cs 		# 파워 업 스킬


┣ 📂 Stage              	# 스테이지 관련 스크립트

 ┃ ┣ 📜 BattleStageController.cs 	# 전투 스테이지 컨트롤러

 ┃ ┣ 📜 Executer.cs 		# 스테이지 아이템 처리기

 ┃ ┣ 📜 ShopStageCnotroller.cs 	# 상점 스테이지 컨트롤러

 ┃ ┣ 📜 StageItemGenerator.cs 	# 스테이지 아이템 생성기


┣ 📂 StateMachine        	# 플레이어 상태 머신 관련 스크립트

 ┃ ┣ 📜 BaseState.cs 		# 기본 상태 상속용

 ┃ ┣ 📜 BaseStateMachine.cs 	# 기본 상태 머신 상속용

 ┃ ┣ 📜 PlayerStateMachine.cs	# 플레이어 상태 머신

┃ ┣ 📂 PlayerStates

 ┃ ┃ ┣ 📜 PlayerBattleState.cs 	# 플레이어 전투 상태

 ┃ ┃ ┣ 📜 PlayerIdleState.cs	# 플레이어 평소 상태

┣ 📂 UI                	# UI  관련 스크립트

 ┃ ┣ 📜 CanvasSample.cs		# 씬 별 캔버스 정리

 ┃ ┣ 📜 CanvasShop.cs		# 씬 별 캔버스 정리

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

 ┃ ┣ 📜 StageClearPanel.cs		#

 ┃ ┣ 📜 RoundTextUI.cs		#

 ┃ ┣ 📜 StageClearPanel.cs		#

 ┃ ┣ 📜 StageSelectCanvas.cs	#

 ┃ ┣ 📜 StartCanvas.cs		#

┃ ┣ 📂 PlayerStates

 ┃ ┃ ┣ 📜 UIReward.cs		#

 ┃ ┃ ┣ 📜 UIShop.cs		#

 ┃ ┃ ┣ 📜 UIStageShow.cs		#

 ┃ ┃ ┣ 📜 UIStart.cs		#

 ┃ ┃ ┣ 📜 UITop.cs		#

 ┃ ┃ ┣ 📜 UIMain.cs		#

 ┃ ┃ ┣ 📜 UIGameOver.cs		#

 ┃ ┃ ┣ 📜 UIPopup.cs		#

---

## 🎮 인게임 구성

뽑기 시스템 제작 - 박성준

스테이지(스테이지 정보를 담고 있는 거), 적 생성 - 안준걸

플레이어, stat 제어, Enemy - 배연두

UI 작업 - 박성주

스킬, 턴제 시스템 - 배리안

---

## ⚙ 주요 시스템

**조작법**
- 뽑기 시스템을 활용한 덱 빌딩 로그라이크

**자원**
-카드 덱
-특성 덱

---

## 🎥 플레이 영상
