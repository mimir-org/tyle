import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Button } from "complib/buttons";
import { Token } from "complib/general";
import { Box } from "complib/layouts";
import { Dialog } from "complib/overlays/dialog/Dialog";

export default {
  title: "Overlays/Dialog",
  component: Dialog,
} as ComponentMeta<typeof Dialog>;

const Template: ComponentStory<typeof Dialog> = (args) => <Dialog {...args} />;

export const Default = Template.bind({});
Default.args = {
  content: "Plain example text",
  title: "Dialog title",
  description: "Description of the dialog's contents",
  children: <Button>Show dialog</Button>,
};

export const WithCustomContent = Template.bind({});
WithCustomContent.args = {
  ...Default.args,
  content: (
    <Box display={"flex"} gap={"8px"} p={"16px"} borderRadius={"5px"} bgColor={"green"}>
      <Token variant={"secondary"}>YOUR</Token>
      <Token>CUSTOM</Token>
      <Token variant={"secondary"}>CONTENT</Token>
    </Box>
  ),
};

export const WithHiddenTitle = Template.bind({});
WithHiddenTitle.args = {
  ...Default.args,
  hideTitle: true,
};

export const WithHiddenTitleAndDescription = Template.bind({});
WithHiddenTitleAndDescription.args = {
  ...WithHiddenTitle.args,
  hideDescription: true,
};
