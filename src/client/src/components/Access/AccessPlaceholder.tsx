import { MotionText } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

const AccessPlaceholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionText variant={"title-large"} color={theme.tyle.color.surface.variant.on} {...theme.tyle.animation.fade}>
      {text}
    </MotionText>
  );
};

export default AccessPlaceholder;
