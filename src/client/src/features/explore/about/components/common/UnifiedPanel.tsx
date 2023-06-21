import { Flexbox, MotionBox } from "complib/layouts";
import { Heading, Text } from "complib/text";
import { useTheme } from "styled-components";
import { State } from "@mimirorg/typelibrary-types";
import { StateBadge } from "../../../../ui/badges/StateBadge";

interface UnifiedPanelProps {
  name: string;
  description: string;
  state: State;
  children?: JSX.Element;
  showName?: boolean;
  showDescription?: boolean;
}

/**
 * Component that displays information about a given terminal.
 *
 * @param props receives all properties of a TerminalItem
 * @constructor
 */
export default function UnifiedPanel({
  name,
  description,
  state,
  children,
  showName,
  showDescription,
}: UnifiedPanelProps) {
  const theme = useTheme();

  return (
    <MotionBox
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.xxxl}
      maxHeight={"100%"}
      {...theme.tyle.animation.fade}
    >
      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xl}>
        {children}
        {showName && (
          <Heading as={"h2"} variant={"title-large"} fontWeight={"500"} useEllipsis ellipsisMaxLines={2}>
            {name}
          </Heading>
        )}
        {showDescription && (
          <Text useEllipsis ellipsisMaxLines={8}>
            {description}
          </Text>
        )}
      </Flexbox>
      <Flexbox gap={theme.tyle.spacing.xl} flexWrap={"wrap"}>
        <StateBadge state={state} />
      </Flexbox>
    </MotionBox>
  );
}
