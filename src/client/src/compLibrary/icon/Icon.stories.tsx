import { Icon } from "./Icon";
import { LibraryIcon } from "../../assets/icons/modules";
import { ComponentStory } from "@storybook/react";

export default {
  title: "Library/Atoms/Icon",
  component: Icon,
  args: {
    src: LibraryIcon,
  },
};

const Template: ComponentStory<typeof Icon> = (args) => <Icon {...args}></Icon>;

export const Default = Template.bind({});
Default.args = {
  size: 15,
};
