import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Default as NodePreview } from "../../../content/home/components/about/components/node/NodePreview.stories";
import { NodePreviewProps } from "../../../content/home/components/about/components/node/NodePreview";
import { Dialog } from "./Dialog";
import { Button } from "../../buttons";

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

export const WithComponentContent = Template.bind({});
WithComponentContent.args = {
  ...Default.args,
  content: <NodePreview {...(NodePreview.args as NodePreviewProps)} />,
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
