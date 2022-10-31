import { ComponentMeta } from "@storybook/react";
import { Button } from "complib/buttons";
import { Tooltip } from "complib/data-display/tooltip/Tooltip";
import { Box, Flexbox } from "complib/layouts";
import { Text } from "complib/text";

export default {
  title: "Data display/Tooltip",
  component: Tooltip,
} as ComponentMeta<typeof Tooltip>;

export const Default = () => (
  <Tooltip content={"The tooltip defaults to top position if there is enough space available"}>
    <Button>Focusable element</Button>
  </Tooltip>
);

export const PlacementRight = () => (
  <Tooltip placement={"right"} content={"Right placement"}>
    <Button>Focusable element</Button>
  </Tooltip>
);

export const PlacementBottom = () => (
  <Tooltip placement={"bottom"} content={"Bottom placement"}>
    <Button>Focusable element</Button>
  </Tooltip>
);

export const PlacementLeft = () => (
  <Tooltip placement={"left"} content={"Left placement"}>
    <Button>Focusable element</Button>
  </Tooltip>
);

export const WithDelay = () => (
  <Tooltip delay={200} content={"Delay of 200ms"}>
    <Button>Focusable element</Button>
  </Tooltip>
);

export const WithOffset = () => (
  <Tooltip offset={50} content={"Offset of 50px"}>
    <Button>Focusable element</Button>
  </Tooltip>
);

export const WithComponentContent = () => (
  <Tooltip
    placement={"right"}
    content={
      <Flexbox gap={"8px"}>
        <Box width={"20px"} height={"20px"} bgColor={"#5fa0ea"} />
        <Text>This tooltip contains other components</Text>
      </Flexbox>
    }
  >
    <Button>Focusable element</Button>
  </Tooltip>
);
