import { MotionText } from "components/Text";
import { useTheme } from "styled-components";

const ApprovalPlaceholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionText variant={"title-large"} color={theme.tyle.color.surface.variant.on} {...theme.tyle.animation.fade}>
      {text}
    </MotionText>
  );
};

export default ApprovalPlaceholder;
