import { Meta, StoryFn } from "@storybook/react";
import { Button } from "complib/buttons";
import { Token } from "complib/general";
import { Box } from "complib/layouts";
import { AlertDialog } from "complib/overlays/alert-dialog/AlertDialog";

export default {
  title: "Overlays/AlertDialog",
  component: AlertDialog,
} as Meta<typeof AlertDialog>;

const Template: StoryFn<typeof AlertDialog> = (args) => <AlertDialog {...args} />;

export const Default = Template.bind({});
Default.args = {
  content: "Plain example text",
  title: "Dialog title",
  description: "Description of the dialog's contents",
  children: <Button>Show dialog</Button>,
  actions: [
    {
      name: "Example action",
      onAction: () => alert("[STORYBOOK] AlertDialog.Action"),
    },
  ],
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
