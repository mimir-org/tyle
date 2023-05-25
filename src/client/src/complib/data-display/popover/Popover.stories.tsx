import { Meta } from "@storybook/react";
import { Button } from "complib/buttons";
import { Divider } from "complib/data-display/divider/Divider";
import { Popover } from "complib/data-display/popover/Popover";
import { Box } from "complib/layouts";
import { Text } from "complib/text";

export default {
  title: "Data display/Popover",
  component: Popover,
} as Meta<typeof Popover>;

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
