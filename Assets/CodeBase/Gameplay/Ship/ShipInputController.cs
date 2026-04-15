using UnityEngine;

public class ShipInputController : MonoBehaviour, IDependency<LevelStateTracker>
{
    public enum ControlMode
    {
        Keyboard,
        Mobile
    }

    [SerializeField] private ControlMode m_ControlMode;

    public void Construct(VirtualGamepad virtualGamepad)
    {
        m_VirtualGamepad = virtualGamepad;
    }

    private SpaceShip m_TargetShip;
    private VirtualGamepad m_VirtualGamepad;

    private LevelStateTracker levelStateTracker;

    public void Construct(LevelStateTracker stateTracker) => levelStateTracker = stateTracker;

    public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;
    
    private bool m_IsMobilePlatform;

    [SerializeField] private float m_MobileDeadzone = 0.2f;

    [SerializeField] private bool m_InvertMobileY = false;

    private void Start()
    {
        m_IsMobilePlatform = Application.isMobilePlatform;

        if (m_IsMobilePlatform)
        {
            SetControlMode(ControlMode.Mobile);
        }
        else
        {
            SetControlMode(ControlMode.Keyboard);
        }
    }

    private void Update()
    {
        if (levelStateTracker == null || levelStateTracker.State != LevelState.Action)
            return;

        if (m_TargetShip == null) return;

        if (m_ControlMode == ControlMode.Keyboard && IsMobileInput())
        {
            SetControlMode(ControlMode.Mobile);
        }
        else if (m_ControlMode == ControlMode.Mobile && IsKeyboardInput())
        {
            SetControlMode(ControlMode.Keyboard);
        }

        if (m_ControlMode == ControlMode.Mobile)
            ControlMobile();
        else
            ControlKeyboard();

    }

    private void SetControlMode(ControlMode mode)
    {
        m_ControlMode = mode;
        
        if (m_VirtualGamepad.VirtualJoystick != null)
        {
            if (mode == ControlMode.Mobile)
            {
                m_VirtualGamepad.VirtualJoystick.gameObject.SetActive(true);
                m_VirtualGamepad.MobileFirePrimary.gameObject.SetActive(true);
                m_VirtualGamepad.MobileFireSecondary.gameObject.SetActive(true);
            }
            else
            {
                m_VirtualGamepad.VirtualJoystick.gameObject.SetActive(false);
                m_VirtualGamepad.MobileFirePrimary.gameObject.SetActive(false);
                m_VirtualGamepad.MobileFireSecondary.gameObject.SetActive(false);
                m_VirtualGamepad.VirtualJoystick.Reset();
            }
        }    
    }

    private bool IsKeyboardInput()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) ||
               Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
               Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
               Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
    }

    private bool IsMobileInput()
    {
        if (m_VirtualGamepad?.VirtualJoystick == null) return false;
        Vector3 v = m_VirtualGamepad.VirtualJoystick.Value;
        return Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y)) > m_MobileDeadzone;
    }

    private void ControlMobile()
    {
        if (m_VirtualGamepad?.VirtualJoystick == null || m_TargetShip == null) return;

        Vector3 joy = m_VirtualGamepad.VirtualJoystick.Value;
        float joyX = Mathf.Clamp(joy.x, -1f, 1f);
        float joyY = Mathf.Clamp(joy.y * (m_InvertMobileY ? -1f : 1f), -1f, 1f);

        float trust = Mathf.Abs(joyY) < m_MobileDeadzone ? 0f : joyY;

        float torque = Mathf.Abs(joyX) < m_MobileDeadzone ? 0f : -joyX;

        m_TargetShip.TrustControl = Mathf.Clamp(trust, -1f, 1f);
        m_TargetShip.TorqueControl = Mathf.Clamp(torque, -1f, 1f);

        if (m_VirtualGamepad.MobileFirePrimary.IsHold == true)
        {
            m_TargetShip.Fire(TurretMode.Primary);
        }

        if (m_VirtualGamepad.MobileFireSecondary.IsHold == true)
        {
            m_TargetShip.Fire(TurretMode.Secondary);
        }
    }

    private void ControlKeyboard()
    {
        float trust = 0.0f;
        float torque = 0.0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            trust = 1.0f;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            trust = -1.0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            torque = 1.0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            torque = -1.0f;

        if (Input.GetKey(KeyCode.Space))
        {
            m_TargetShip.Fire(TurretMode.Primary);
        }

        if (Input.GetKey(KeyCode.X))
        {
            m_TargetShip.Fire(TurretMode.Secondary);
        }

        m_TargetShip.TrustControl = trust;
        m_TargetShip.TorqueControl = torque;
    }

}
