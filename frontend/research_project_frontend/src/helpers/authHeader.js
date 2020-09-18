export function authHeader() {
    let user = localStorage.getItem('user');

    if(user.token) {
        return {'Authorization': 'Bearer ' + user.token};
    } else {
        return {};
    }
}

export default authHeader;