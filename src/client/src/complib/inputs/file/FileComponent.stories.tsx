import { ComponentMeta, ComponentStory } from "@storybook/react";
import { FileComponent, FileInfo } from "./FileComponent";

const files = [] as FileInfo[];

export default {
  title: "Inputs/File",
  component: FileComponent,
} as ComponentMeta<typeof FileComponent>;

const Template: ComponentStory<typeof FileComponent> = (args) => <FileComponent {...args} />;

export const Default = Template.bind({});
Default.args = {
  value: files,
  placeholder: "This is the placeholder",
  tooltip: "This is the tooltip",
};
