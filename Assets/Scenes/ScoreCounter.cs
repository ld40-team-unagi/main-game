
public static class ScoreCounte {
	public static void AddScore(uint s){
		score += s;
	}
	public static uint CurrentScore(){
		return score;
	}
	public static void Rest(){
		score = 0;
	}
	private static uint score = 0;
}
