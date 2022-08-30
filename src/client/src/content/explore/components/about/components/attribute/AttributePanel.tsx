import { useTheme } from "styled-components";
import { Token } from "../../../../../../complib/general";
import { Flexbox, MotionBox } from "../../../../../../complib/layouts";
import { Heading, Text } from "../../../../../../complib/text";
import { AttributePreview } from "../../../../../common/attribute";
import { AttributeItem } from "../../../../../types/AttributeItem";

/**
 * Component that displays information about a given attribute.
 *
 * @param props receives all properties of an AttributeItem
 * @constructor
 */
export const AttributePanel = (props: AttributeItem) => {
  const theme = useTheme();
  const showContentButtons = props.contents.length > 0;

  return (
    <MotionBox
      flex={1}
      display={"flex"}
      flexDirection={"column"}
      gap={theme.tyle.spacing.xxxl}
      maxHeight={"100%"}
      overflow={"hidden"}
      {...theme.tyle.animation.fade}
    >
      <AttributePreview {...props} variant={"large"} />

      <Flexbox flexDirection={"column"} gap={theme.tyle.spacing.xl}>
        <Heading as={"h2"} variant={"title-large"} fontWeight={"500"} useEllipsis ellipsisMaxLines={2}>
          {props.name}
        </Heading>
        <Text useEllipsis ellipsisMaxLines={8}>
          {props.description}
        </Text>
      </Flexbox>
      <Flexbox gap={theme.tyle.spacing.xl} flexWrap={"wrap"}>
        {props.tokens && props.tokens.map((t, i) => <Token key={i}>{t}</Token>)}
      </Flexbox>

      {showContentButtons &&
        props.contents.map((info) => (
          <>
            <Heading as={"h3"} variant={"body-large"} color={theme.tyle.color.sys.surface.on}>
              {info.name}
            </Heading>
            <Flexbox flexWrap={"wrap"} gap={theme.tyle.spacing.xl}>
              {Object.keys(info.descriptors).map((k, i) => (
                <Token key={i} variant={"secondary"}>
                  {info.descriptors[k]}
                </Token>
              ))}
            </Flexbox>
          </>
        ))}
    </MotionBox>
  );
};
