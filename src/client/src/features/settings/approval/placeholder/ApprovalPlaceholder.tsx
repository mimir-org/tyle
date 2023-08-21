import { MotionText } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

export const ApprovalPlaceholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionText
      variant={"title-large"}
      color={theme.mimirorg.color.surface.variant.on}
      {...theme.mimirorg.animation.fade}
    >
      {text}
    </MotionText>
  );
};
