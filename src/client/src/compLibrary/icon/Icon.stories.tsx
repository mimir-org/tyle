import { Icon } from "./Icon";
import { LibraryIcon } from "../../assets/icons/modules";
import { ComponentStory } from "@storybook/react";

export default {
  title: "Library/Atoms/Icon",
  component: Icon,
};

const Template: ComponentStory<typeof Icon> = (args) => <Icon {...args}></Icon>;

export const Default = Template.bind({});
Default.args = {
  src: LibraryIcon,
  size: 16,
};
