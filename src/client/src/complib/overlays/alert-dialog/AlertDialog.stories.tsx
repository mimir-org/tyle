import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Default as NodePreview } from "../../../content/home/components/about/components/node/NodePreview.stories";
import { NodePreviewProps } from "../../../content/home/components/about/components/node/NodePreview";
import { AlertDialog } from "./AlertDialog";
import { Button } from "../../buttons";

export default {
  title: "Overlays/AlertDialog",
  component: AlertDialog,
} as ComponentMeta<typeof AlertDialog>;

const Template: ComponentStory<typeof AlertDialog> = (args) => <AlertDialog {...args} />;

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

export const WithComponentContent = Template.bind({});
WithComponentContent.args = {
  ...Default.args,
  content: <NodePreview {...(NodePreview.args as NodePreviewProps)} />,
};

export const WithDangerAction = Template.bind({});
WithDangerAction.args = {
  ...Default.args,
  content: <NodePreview {...(NodePreview.args as NodePreviewProps)} />,
  title: "Are you sure you want to delete this entity?",
  description: "Deletions are irreversible, be absolutely sure before proceeding.",
  actions: [
    {
      name: "Delete entity",
      danger: true,
      onAction: () => alert("[STORYBOOK] AlertDialog.Action"),
    },
  ],
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
