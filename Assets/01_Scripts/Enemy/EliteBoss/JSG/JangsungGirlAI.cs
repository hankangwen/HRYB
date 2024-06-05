using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JangsungGirlAI : AISetter
{
	public Actor _friend;

	private const string _MumukNansa = "mumuk";
	private const string _BarrierPattonname = "Barrier";
	private const string _RootPatton = "Root";


	public bool _isStart =false;
	public void DieEvent()
	{
		//self.anim.ResetStatus();
		StopExamine();
		GetComponent<BoxCollider>().enabled = false;
		_friend.GetComponent<JangsungLifeModule>().BarrierOff();
		
	}

	protected override void StartInvoke()
    {
	    if (_isStart)
	    {
			JangsungGirlAttack _att = self.atk as JangsungGirlAttack;
		    JangsungGirlLifeModule _life = self.life as JangsungGirlLifeModule;
			head = new Selecter();
			_life._dieEvent += DieEvent;
			GetComponent<BoxCollider>().enabled = true;

			#region 보호막

			Waiter waitBarrier = new Waiter(0.5f, true);
		    IsOutRange isBarrier = new IsOutRange(self, _friend.transform, BarrierRange, null, () =>
		    {
			    // Barrier 에니메이션 실행
			    // AttackMoudle에서 이름 셋팅
			    _att.SetAttackType(_BarrierPattonname);
			    waitBarrier.StartReady();
		    });
		    Attacker BarrierGet = new Attacker(self, () =>
		    {
			    waitBarrier.ResetReady();
			    // AttackMoudle에서 이름 셋팅 << Attack에 유기하기
			    _life.BarrierON(5);
				if (_friend.life.isDead == false)
				{
					_friend.GetComponent<JangsungLifeModule>().BarrierON();
				}
			    Debug.LogError("보호막패턴");
			    self.anim.SetAttackTrigger();
			    self.anim.Animators.SetBool(_BarrierPattonname, true);
			    StopExamine(); // LifeModule에서 풀어줘야됨 << 몇회 피격시니까
		    });

		    Sequencer BarrierPatton = new Sequencer();

		    BarrierPatton.connecteds.Add(isBarrier);
		    BarrierPatton.connecteds.Add(waitBarrier);
		    BarrierPatton.connecteds.Add(BarrierGet);


		    #endregion

		    #region 으아악 뿌리다

		    Waiter waitRootAttack = new Waiter(30.0f);
		    IsInRange isRootAttack = new IsInRange(self, player.transform, RootRange, null, () =>
		    {
			    // AttackMoudle에서 이름 셋팅 << Attack에 유기하기?
			    _att.SetAttackType(_RootPatton);
			    waitRootAttack.StartReady();
		    });

		    Attacker RootAttack = new Attacker(self, () =>
		    {
			    waitRootAttack.ResetReady();
			    _att.SetAttackType(_RootPatton);
			    // 이름 셋팅 함 더하기
			    self.anim.Animators.SetBool(_RootPatton, true);
			    self.anim.SetAttackTrigger();
			    Debug.LogError("땅바닥");
			    _friend.GetComponent<JangsungLifeModule>().BarrierON();
			    StopExamine(); // 패턴 끝나거나 (코루틴) or 캔슬시 코루틴 끊고 Start 해주기 
		    });

		    Sequencer RootPatton = new Sequencer();
		    RootPatton.connecteds.Add(isRootAttack);
		    RootPatton.connecteds.Add(waitRootAttack);
		    RootPatton.connecteds.Add(RootAttack);

		    #endregion

		    #region 묘목

		    Waiter waitSeedAttack = new Waiter(12.0f);
		    IsInRange isSeedAttack = new IsInRange(self, player.transform, SeedRange, null, () =>
		    {
			    // AttackMoudle에서 이름 셋팅 << Attack에 유기하기?
			    _att.SetAttackType(_MumukNansa);
			    waitSeedAttack.StartReady();
		    });
		    Attacker SeedAttack = new Attacker(self, () =>
		    {
			    waitSeedAttack.ResetReady();
			    _att.SetAttackType(_MumukNansa);
			    self.anim.Animators.SetBool(_MumukNansa, true);
			    self.anim.SetAttackTrigger();
			    Debug.LogError("뮤뮥");
			    StopExamine();
		    });

		    Sequencer SeedPatton = new Sequencer();
		    SeedPatton.connecteds.Add(isSeedAttack);
		    SeedPatton.connecteds.Add(waitSeedAttack);
		    SeedPatton.connecteds.Add(SeedAttack);

		    #endregion

		    head.connecteds.Add(BarrierPatton);
		    head.connecteds.Add(RootPatton);
		    head.connecteds.Add(SeedPatton);
		    Debug.LogError("실행중");
		    StartExamine();
		}
		else
		{
			StartCoroutine(WaitStart());
		}
	}

	IEnumerator WaitStart()
	{
		head = null;
		yield return new WaitUntil(() => _isStart);
		yield return new WaitForSeconds(1f);
		StartInvoke();
	}
	protected override void UpdateInvoke()
    {
		if(self.life.isDead==false)
		{
			//Debug.LogError("도는중이긴함");
			Vector3 lookPos = player.transform.position - transform.position;
			lookPos.y = transform.position.y;
			transform.rotation = Quaternion.LookRotation(lookPos);
			transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
		}
	}


	protected override void Update()
	{
		if (!stopped && head != null)
		{
			head.Examine();
		}

		UpdateInvoke();
	}

	public float BarrierRange()
    {
	    return 25f;
    }

    public float RootRange()
    {
	    return 10f;
    }
    
    public float SeedRange()
    {
	    return 25f;
    }
	public override void ResetStatus()
	{
		base.ResetStatus();
		_isStart = false;
		StartInvoke();
		self.life.ResetStatus();
	}
}
