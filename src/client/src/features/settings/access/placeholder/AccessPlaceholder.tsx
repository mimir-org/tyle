import { MotionText } from "complib/text";
import { useTheme } from "styled-components";

export const AccessPlaceholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionText variant={"title-large"} color={theme.tyle.color.sys.surface.variant.on} {...theme.tyle.animation.fade}>
      {text}
    </MotionText>
  );
};
