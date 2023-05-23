import { MotionFlexbox } from "complib/layouts";
import { Text } from "complib/text";
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
export const SearchPlaceholder = ({ title, subtitle, tips }: SearchPlaceholderProps) => {
  const theme = useTheme();

  return (
    <MotionFlexbox layout flexDirection={"column"} gap={theme.tyle.spacing.xl} {...theme.tyle.animation.fade}>
      <Text variant={"title-large"} color={theme.tyle.color.sys.surface.variant.on} wordBreak={"break-all"}>
        {title}
      </Text>
      <Text variant={"label-large"} color={theme.tyle.color.sys.primary.base}>
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
