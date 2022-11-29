import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Processing } from "features/auth/common/processing/Processing";

export default {
  title: "Features/Auth/Common/Processing",
  component: Processing,
} as ComponentMeta<typeof Processing>;

const Template: ComponentStory<typeof Processing> = (args) => <Processing {...args} />;

export const Default = Template.bind({});
Default.args = {
  children: "Now loading",
};
