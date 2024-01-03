import { Text } from "@mimirorg/component-library";
import { MotionFlexbox } from "components/Flexbox";
import { useTheme } from "styled-components";

/**
 * Component for presenting content while the user has not selected any item to display information about.
 *
 * @param text
 * @constructor
 */
const AboutPlaceholder = ({ text }: { text: string }) => {
  const theme = useTheme();

  return (
    <MotionFlexbox flex={1} justifyContent={"center"} alignItems={"center"} {...theme.tyle.animation.fade}>
      <Text variant={"title-large"} color={theme.tyle.color.surface.on}>
        {text}
      </Text>
    </MotionFlexbox>
  );
};

export default AboutPlaceholder;
