import { ComponentMeta } from "@storybook/react";
import { Button } from "../../buttons";
import { Box } from "../../layouts";
import { Text } from "../../text";
import { Divider } from "../divider/Divider";
import { Popover } from "./Popover";

export default {
  title: "Data display/Popover",
  component: Popover,
} as ComponentMeta<typeof Popover>;

const Content = () => (
  <Box display={"flex"} flexDirection={"column"} gap={"8px"} maxWidth={"200px"}>
    <Text variant={"title-medium"}>Popover</Text>
    <Divider />
    <Text>
      Popovers are usually used to provide an element with extra information. They are often used for more complex
      information than what you present with a regular tooltip.
    </Text>
  </Box>
);

export const Default = () => (
  <Popover content={<Content />}>
    <Button>Focusable element</Button>
  </Popover>
);

export const PlacementRight = () => (
  <Popover placement={"right"} content={<Content />}>
    <Button>Focusable element</Button>
  </Popover>
);

export const PlacementBottom = () => (
  <Popover placement={"bottom"} content={<Content />}>
    <Button>Focusable element</Button>
  </Popover>
);

export const PlacementLeft = () => (
  <Popover placement={"left"} content={<Content />}>
    <Button>Focusable element</Button>
  </Popover>
);

export const WithOffset = () => (
  <Popover offset={50} content={<Content />}>
    <Button>Focusable element</Button>
  </Popover>
);
