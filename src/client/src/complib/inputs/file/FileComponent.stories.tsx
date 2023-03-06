import { ComponentMeta, ComponentStory } from "@storybook/react";
import { FileComponent, FileInfo } from "./FileComponent";

const file = {
  fileName: "",
  fileSize: 0,
  file: null,
  contentType: ""
} as FileInfo;

export default {
  title: "Inputs/File",
  component: FileComponent,
} as ComponentMeta<typeof FileComponent>;

const Template: ComponentStory<typeof FileComponent> = (args) => <FileComponent {...args} />;

export const Default = Template.bind({});
Default.args = {
  value: file,
  tooltip: "This is the tooltip",
};
