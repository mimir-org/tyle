import { useTheme } from "styled-components";
import { MotionFlexbox } from "../../../../../complib/layouts";
import { Text } from "../../../../../complib/text";

/**
 * Component for presenting content while the user has not selected any item to display information about.
 *
 * @param text
 * @constructor
 */
export const AboutPlaceholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionFlexbox flex={1} justifyContent={"center"} alignItems={"center"} {...theme.tyle.animation.fade}>
      <Text variant={"title-large"} color={theme.tyle.color.sys.surface.on}>
        {text}
      </Text>
    </MotionFlexbox>
  );
};
