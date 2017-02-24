public class KeyboardSettings
{
    public string
        AnalogUp,
        AnalogDown,
        AnalogLeft,
        AnalogRight,
        Fire1,
        Fire2,
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Submit;

    public void SetDefault()
    {
        AnalogUp = "AnalogUp";
        AnalogDown = "AnalogDown";
        AnalogLeft = "AnalogLeft";
        AnalogRight = "AnalogRight";
        Fire1 = "Fire1";
        Fire2 = "Fire2";
        Skill1 = "Skill1";
        Skill2 = "Skill2";
        Skill3 = "Skill3";
        Skill4 = "Skill4";
        Submit = "Submit";
    }

    public string GetSkillKey(int skill)
    {
        switch(skill)
        {
            case 1:
                return Skill1;
            case 2:
                return Skill2;
            case 3:
                return Skill3;
            case 4:
                return Skill4;
        }
        return null;
    }
}
