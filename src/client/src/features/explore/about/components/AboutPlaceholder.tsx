import { MotionFlexbox, Text } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

/**
 * Component for presenting content while the user has not selected any item to display information about.
 *
 * @param text
 * @constructor
 */
export const AboutPlaceholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionFlexbox flex={1} justifyContent={"center"} alignItems={"center"} {...theme.mimirorg.animation.fade}>
      <Text variant={"title-large"} color={theme.mimirorg.color.surface.on}>
        {text}
      </Text>
    </MotionFlexbox>
  );
};
