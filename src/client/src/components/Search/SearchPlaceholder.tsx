import { MotionFlexbox, Text } from "@mimirorg/component-library";
import { useTheme } from "styled-components";

interface SearchPlaceholderProps {
  title: string;
  subtitle: string;
  tips: string[];
}

/**
 * Component serves as a placeholder for when there are no search results available.
 *
 * @param title
 * @param subtitle
 * @param tips
 * @constructor
 */
const SearchPlaceholder = ({ title, subtitle, tips }: SearchPlaceholderProps) => {
  const theme = useTheme();

  return (
    <MotionFlexbox layout flexDirection={"column"} gap={theme.tyle.spacing.xl} {...theme.tyle.animation.fade}>
      <Text variant={"title-large"} color={theme.tyle.color.surface.variant.on} wordBreak={"break-all"}>
        {title}
      </Text>
      <Text variant={"label-large"} color={theme.tyle.color.primary.base}>
        {subtitle}
      </Text>
      <ul>
        {tips.map((tip, i) => (
          <li key={i + tip}>{tip}</li>
        ))}
      </ul>
    </MotionFlexbox>
  );
};

export default SearchPlaceholder;
