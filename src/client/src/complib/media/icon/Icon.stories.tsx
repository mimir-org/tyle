import { ComponentStory } from "@storybook/react";
import { LibraryIcon } from "complib/assets";
import { Icon } from "./Icon";

export default {
  title: "Media/Icon",
  component: Icon,
};

const Template: ComponentStory<typeof Icon> = (args) => <Icon {...args}></Icon>;

export const Default = Template.bind({});
Default.args = {
  src: LibraryIcon,
  size: 16,
};
